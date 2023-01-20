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
            if (!MainPage.ValidateTeamNumber(TeamNumber.Text)) return;
            if (!MainPage.ValidateMatchNumber(MatchNumber.Text)) return;
            if (!MainPage.ValidateScoutName(ScoutName.Text)) return;
            TeamNumber.Text = int.Parse(TeamNumber.Text).ToString();
            MatchNumber.Text = int.Parse(MatchNumber.Text).ToString();
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