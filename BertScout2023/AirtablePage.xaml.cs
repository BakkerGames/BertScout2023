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
            foreach (TeamMatch item in matches
                .OrderBy(x => $"{x.MatchNumber,3}{x.MatchNumber,4}"))
            {
                if (AirtableResults.Text.Length > 0)
                    AirtableResults.Text += "\r\n";
                AirtableResults.Text += $"{item.MatchNumber,4} - {item.TeamNumber,4}";
            }
            var count = await AirtableService.AirtableSendRecords(matches);
            if (AirtableResults.Text.Length > 0)
                AirtableResults.Text += "\r\n";
            AirtableResults.Text += $"{count} record(s) sent to Airtable";
        }
        catch (Exception ex)
        {
            if (AirtableResults.Text.Length > 0)
                AirtableResults.Text += "\r\n";
            AirtableResults.Text += ex.Message;
        }
    }

    private void VerticalStackLayout_SizeChanged(object sender, EventArgs e)
    {
        ScrollResults.HeightRequest = cp.Height - ScrollResults.Y;
    }
}