using System.Text;

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
            if (TeamNumber.Text == "" || MatchNumber.Text == "")
            {
                return;
            }
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