namespace BertScout2023;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void Start_Clicked(object sender, EventArgs e)
    {
        if (Start.Text == "Start")
        {
            if (!ValidateTeamNumber(TeamNumber.Text)) return;
            if (!ValidateMatchNumber(MatchNumber.Text)) return;
            if (!ValidateScoutName(ScoutName.Text)) return;
            TeamNumber.Text = int.Parse (TeamNumber.Text).ToString();
            MatchNumber.Text = int.Parse (MatchNumber.Text).ToString();
            EnableTopRow(false);
        }
        else if (Start.Text == "Save")
        {
            TeamNumber.Text = "";
            MatchNumber.Text = "";
            ClearAllFields();
            EnableTopRow(true);
            TeamNumber.Focus();
        }
    }
}