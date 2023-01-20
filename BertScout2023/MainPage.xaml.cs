using BertScout2023.Databases;
using BertScout2023.Models;

namespace BertScout2023;

public partial class MainPage : ContentPage
{
    private readonly LocalDatabase db = new();

    private TeamMatch item = null;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void Start_Clicked(object sender, EventArgs e)
    {
        if (Start.Text == "Start")
        {
            // check that all fields are valid
            if (!ValidateTeamNumber(TeamNumber.Text)) return;
            if (!ValidateMatchNumber(MatchNumber.Text)) return;
            if (!ValidateScoutName(ScoutName.Text)) return;
            // get integer values for later use
            var team = int.Parse(TeamNumber.Text);
            var match = int.Parse(MatchNumber.Text);
            // update screen fields without leading zeros
            TeamNumber.Text = team.ToString();
            MatchNumber.Text = match.ToString();
            // get existing record
            item = await db.GetTeamMatchAsync(team, match);
            // if not found, create new record
            item ??= new()
                {
                    TeamNumber = team,
                    MatchNumber = match,
                    ScoutName = ScoutName.Text,
                };
            // show the values on the screen
            FillFields(item);
            // disable the top row while entering
            EnableTopRow(false);
        }
        else if (Start.Text == "Save")
        {
            // store the screen fields in the record
            StoreFields(item);
            // save to database
            item.Changed = true;
            await db.SaveItemAsync(item);
            // prepare for next match
            TeamNumber.Text = "";
            var match = int.Parse(MatchNumber.Text);
            MatchNumber.Text = (match + 1).ToString();
            ClearAllFields();
            // re-enable top row and focus on team number
            EnableTopRow(true);
            TeamNumber.Focus();
        }
    }
}