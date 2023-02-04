using BertScout2023.Databases;
using BertScout2023.Models;

namespace BertScout2023;

public partial class MainPage : ContentPage
{
    private readonly LocalDatabase db = new();

    private TeamMatch item = new();

    public MainPage()
    {
        InitializeComponent();
        CommentPicker.Items.Clear();
        foreach (string s in CommentList)
        {
            CommentPicker.Items.Add(s);
        }
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
                    Comments = "",
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

    private void CommentPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CommentPicker.SelectedIndex < 0)
            return;
        if (Comments.Text == null)
            Comments.Text = "";
        else if (Comments.Text.Length > 0)
            Comments.Text += " ";
        Comments.Text += CommentPicker.SelectedItem.ToString();
        CommentPicker.SelectedIndex = -1;
    }

    private void ButtonAutoCubeTopPlus_Clicked(object sender, EventArgs e)
    {
        item.Auto_Cubes_Top++;
        LabelAutoCubeTop.Text = item.Auto_Cubes_Top.ToString();
    }

    private void ButtonAutoCubeMidPlus_Clicked(object sender, EventArgs e)
    {
        item.Auto_Cubes_Middle++;
        LabelAutoCubeMid.Text = item.Auto_Cubes_Middle.ToString();
    }

    private void ButtonAutoCubeLowPlus_Clicked(object sender, EventArgs e)
    {
        item.Auto_Cubes_Bottom++;
        LabelAutoCubeLow.Text = item.Auto_Cubes_Bottom.ToString();
    }

    private void ButtonAutoConeTopPlus_Clicked(object sender, EventArgs e)
    {
        item.Auto_Cones_Top++;
        LabelAutoConeTop.Text = item.Auto_Cones_Top.ToString();
    }

    private void ButtonAutoConeMidPlus_Clicked(object sender, EventArgs e)
    {
        item.Auto_Cones_Middle++;
        LabelAutoConeMid.Text = item.Auto_Cones_Middle.ToString();
    }

    private void ButtonAutoConeLowPlus_Clicked(object sender, EventArgs e)
    {
        item.Auto_Cones_Bottom++;
        LabelAutoConeLow.Text = item.Auto_Cones_Bottom.ToString();
    }

    private void ButtonAutoMobility_Clicked(object sender, EventArgs e)
    {
        item.Auto_Mobility = !item.Auto_Mobility;
        switch (item.Auto_Mobility)
        {
            case false:
                ButtonAutoMobility.BackgroundColor = Colors.Gray;
                break;
            case true:
                ButtonAutoMobility.BackgroundColor = Colors.Green;
                break;
        }
    }

    private void ButtonAutoDocked_Clicked(object sender, EventArgs e)
    {
        item.Auto_Docked = !item.Auto_Docked;
        switch (item.Auto_Docked)
        {
            case false:
                ButtonAutoDocked.BackgroundColor = Colors.Gray;
                break;
            case true:
                ButtonAutoDocked.BackgroundColor = Colors.Green;
                break;
        }
    }

    private void ButtonAutoEngaged_Clicked(object sender, EventArgs e)
    {
        item.Auto_Engaged = !item.Auto_Engaged;
        switch (item.Auto_Engaged)
        {
            case false:
                ButtonAutoEngaged.BackgroundColor = Colors.Gray;
                break;
            case true:
                ButtonAutoEngaged.BackgroundColor = Colors.Green;
                break;
        }
    }

    private void ButtonTeleCubeTopPlus_Clicked(object sender, EventArgs e)
    {
        item.Tele_Cubes_Top++;
        LabelTeleCubeTop.Text = item.Tele_Cubes_Top.ToString();
    }

    private void ButtonTeleCubeMidPlus_Clicked(object sender, EventArgs e)
    {
        item.Tele_Cubes_Middle++;
        LabelTeleCubeMid.Text = item.Tele_Cubes_Middle.ToString();
    }

    private void ButtonTeleCubeLowPlus_Clicked(object sender, EventArgs e)
    {
        item.Tele_Cubes_Bottom++;
        LabelTeleCubeLow.Text = item.Tele_Cubes_Bottom.ToString();
    }

    private void ButtonTeleConeTopPlus_Clicked(object sender, EventArgs e)
    {
        item.Tele_Cones_Top++;
        LabelTeleConeTop.Text = item.Tele_Cones_Top.ToString();
    }

    private void ButtonTeleConeMidPlus_Clicked(object sender, EventArgs e)
    {
        item.Tele_Cones_Middle++;
        LabelTeleConeMid.Text = item.Tele_Cones_Middle.ToString();
    }

    private void ButtonTeleConeLowPlus_Clicked(object sender, EventArgs e)
    {
        item.Tele_Cones_Bottom++;
        LabelTeleConeLow.Text = item.Tele_Cones_Bottom.ToString();
    }

    private void ButtonEndgameParked_Clicked(object sender, EventArgs e)
    {
        item.Endgame_Parked = !item.Endgame_Parked;
        switch (item.Endgame_Parked)
        {
            case false:
                ButtonEndgameParked.BackgroundColor = Colors.Gray;
                break;
            case true:
                ButtonEndgameParked.BackgroundColor = Colors.Green;
                break;
        }
    }

    private void ButtonEndgameDocked_Clicked(object sender, EventArgs e)
    {
        item.Endgame_Docked = !item.Endgame_Docked;
        switch (item.Endgame_Docked)
        {
            case false:
                ButtonEndgameDocked.BackgroundColor = Colors.Gray;
                break;
            case true:
                ButtonEndgameDocked.BackgroundColor = Colors.Green;
                break;
        }
    }

    private void ButtonEndgameEngaged_Clicked(object sender, EventArgs e)
    {
        item.Endgame_Engaged = !item.Endgame_Engaged;
        switch (item.Endgame_Engaged)
        {
            case false:
                ButtonEndgameEngaged.BackgroundColor = Colors.Gray;
                break;
            case true:
                ButtonEndgameEngaged.BackgroundColor = Colors.Green;
                break;
        }
    }

    private void Comments_TextChanged(object sender, TextChangedEventArgs e)
    {
        item.Comments = Comments?.Text ?? "";
    }
}