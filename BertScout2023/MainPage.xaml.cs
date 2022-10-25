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
            if (!int.TryParse(TeamNumber.Text, out int teamNumber) || !int.TryParse(MatchNumber.Text, out int matchNumber))
                return;
            if (teamNumber < 1 || matchNumber < 1)
                return;
            FormBody.IsVisible = true;
            Start.Text = "Save";
        }
        else if (Start.Text == "Save")
        {
            FormBody.IsVisible = false;
            Start.Text = "Start";
            TeamNumber.Text = "";
            MatchNumber.Text = "";
        }
    }
}