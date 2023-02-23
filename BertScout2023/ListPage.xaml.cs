using BertScout2023.Databases;
using BertScout2023.Models;

namespace BertScout2023;

public partial class ListPage : ContentPage
{
    private readonly LocalDatabase db = new();

    public ListPage()
    {
        InitializeComponent();
        ListResults.Text = "";
    }

    private async void ShowMatchesAsync()
    {
        ListResults.Text = "";
        List<TeamMatch> matches = await db.GetItemsAsync();
        foreach (TeamMatch item in matches
            .OrderBy(x => $"{x.MatchNumber,3}{x.MatchNumber,4}"))
        {
            if (ListResults.Text.Length > 0)
                ListResults.Text += "\r\n";
            ListResults.Text += $"Match {item.MatchNumber,4} - Team {item.TeamNumber,4} - {item.ScoutName}";
        }
    }

    private void VerticalStackLayout_SizeChanged(object sender, EventArgs e)
    {
        ScrollResults.HeightRequest = cpListMatches.Height - ScrollResults.Y;
    }

    private void ShowMatchButton_Clicked(object sender, EventArgs e)
    {
        ShowMatchesAsync();
    }
}