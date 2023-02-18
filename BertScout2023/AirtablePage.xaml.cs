using BertScout2023.Databases;
using BertScout2023.Models;

namespace BertScout2023;

public partial class AirtablePage : ContentPage
{
    private readonly LocalDatabase db = new();

    public AirtablePage()
    {
        InitializeComponent();
    }

    private async void AirtableSend_Clicked(object sender, EventArgs e)
    {
        try
        {
            AirtableResults.Text = "";
            List<TeamMatch> matches = await db.GetItemsAsync();
            var count = await AirtableService.AirtableSendRecords(matches);
            var showS = (count == 1) ? "" : "s";
            AirtableUpdatedLabel.Text = $"{count} record{showS} sent to Airtable";
            foreach (TeamMatch item in matches
                  .OrderBy(x => $"{x.MatchNumber,3}{x.MatchNumber,4}"))
            {
                if (item.Changed)
                {
                    item.Changed = false;
                    await db.SaveItemAsync(item);
                    if (AirtableResults.Text.Length > 0)
                        AirtableResults.Text += "\r\n";
                    AirtableResults.Text += $"Match {item.MatchNumber,4} - Team {item.TeamNumber,4}";
                }
            }
        }
        catch (Exception ex)
        {
            AirtableResults.Text = ex.Message;
        }
    }

    private void VerticalStackLayout_SizeChanged(object sender, EventArgs e)
    {
        ScrollResults.HeightRequest = cp.Height - ScrollResults.Y;
    }
}