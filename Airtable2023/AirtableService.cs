using AirtableApiClient;
using Data2023;
using System.Reflection;

namespace Airtable2023;

public class AirtableService
{
    public static async Task<int> AirtableSendRecords(List<TeamMatch> matches)
    {
        int NewCount = 0;
        int UpdatedCount = 0;
        List<Fields> newRecordList = new();
        List<IdFields> updatedRecordList = new();
        FieldInfo[] myFieldInfo;
        Type myType = typeof(TeamMatch);
        myFieldInfo = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

        using AirtableBase airtableBase = new(Constants.AIRTABLE_KEY, Constants.AIRTABLE_BASE);

        foreach (TeamMatch match in matches)
        {
            if (match.Deleted)
            {
                continue;
            }
            if (match.Uuid == null) continue;
            if (string.IsNullOrEmpty(match.AirtableId))
            {
                Fields fields = new();
                foreach (FieldInfo fi in myFieldInfo)
                {
                    // name is "<name>stuff", so just get the name part
                    int pos1 = fi.Name.IndexOf('<') + 1;
                    int pos2 = fi.Name.IndexOf('>');
                    string name = fi.Name[pos1..pos2];
                    // these fields are not in airtable
                    if (name.ToLower() == "id") continue;
                    if (name.ToLower() == "airtableid") continue;
                    if (name.ToLower() == "changed") continue;
                    if (name.ToLower() == "deleted") continue;
                    fields.AddField(name, fi.GetValue(match));
                }
                newRecordList.Add(fields);
            }
            else if (match.Changed && !match.Deleted)
            {
                IdFields idFields = new(match.AirtableId);
                foreach (FieldInfo fi in myFieldInfo)
                {
                    // name is "<name>stuff", so just get the name part
                    int pos1 = fi.Name.IndexOf('<') + 1;
                    int pos2 = fi.Name.IndexOf('>');
                    string name = fi.Name[pos1..pos2];
                    // these fields are not in airtable
                    if (name.ToLower() == "id") continue;
                    if (name.ToLower() == "airtableid") continue;
                    if (name.ToLower() == "changed") continue;
                    if (name.ToLower() == "deleted") continue;
                    idFields.AddField(name, fi.GetValue(match));
                }
                updatedRecordList.Add(idFields);
            }

            if (newRecordList.Count > 0)
            {
                int tempCount = await AirtableSendNewRecords(airtableBase, newRecordList, matches);
                if (tempCount < 0)
                {
                    return -1; // error, exit out
                }
                NewCount += tempCount;
            }

            if (updatedRecordList.Count > 0)
            {
                int tempCount = await AirtableSendUpdatedRecords(airtableBase, updatedRecordList);
                if (tempCount < 0)
                {
                    return -1; // error, exit out
                }
                UpdatedCount += tempCount;
            }
        }

        return NewCount + UpdatedCount;
    }

    private static async Task<int> AirtableSendNewRecords(
        AirtableBase airtableBase,
        List<Fields> newRecordList,
        List<TeamMatch> matches)
    {
        AirtableCreateUpdateReplaceMultipleRecordsResponse result;
        List<Fields> sendList = new();
        int finalCount = 0;
        while (newRecordList.Count > 0)
        {
            sendList.Clear();
            do
            {
                sendList.Add(newRecordList[0]);
                newRecordList.RemoveAt(0);
            } while (newRecordList.Count > 0 && sendList.Count < 10);
            result = await airtableBase.CreateMultipleRecords("TeamMatch", sendList.ToArray());
            if (!result.Success)
            {
                return -1;
            }
            foreach (AirtableRecord rec in result.Records)
            {
                foreach (TeamMatch match in matches)
                {
                    if (match.Uuid == null) continue;
                    if (match.Uuid == rec.GetField("Uuid")?.ToString())
                    {
                        match.AirtableId = rec.Id;
                        finalCount++;
                        break;
                    }
                }
            }
            if (newRecordList.Count > 0)
            {
                // can only send 5 batches per second - make sure that doesn't happen
                Thread.Sleep(500);
            }
        }
        return finalCount;
    }

    private static async Task<int> AirtableSendUpdatedRecords(
        AirtableBase airtableBase,
        List<IdFields> updatedRecordList)
    {
        AirtableCreateUpdateReplaceMultipleRecordsResponse result;
        List<IdFields> sendList = new();
        int finalCount = 0;
        while (updatedRecordList.Count > 0)
        {
            sendList.Clear();
            do
            {
                sendList.Add(updatedRecordList[0]);
                updatedRecordList.RemoveAt(0);
            } while (updatedRecordList.Count > 0 && sendList.Count < 10);
            result = await airtableBase.UpdateMultipleRecords("TeamMatch", sendList.ToArray());
            if (!result.Success)
            {
                return -1;
            }
            foreach (AirtableRecord rec in result.Records)
            {
                finalCount++;
            }
            if (updatedRecordList.Count > 0)
            {
                // can only send 5 batches per second, make sure that doesn't happen
                Thread.Sleep(500);
            }
        }
        return finalCount;
    }
}