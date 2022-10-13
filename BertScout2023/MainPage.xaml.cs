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
        if (string.IsNullOrEmpty(TeamNumber.Text))
        {
            StartLabel.Text = "Please enter a team number";
        }
        else
        {
            StartLabel.Text = "Hello Team " + TeamNumber.Text;
        }
    }

    private void TeamNumber_TextChanged(object sender, TextChangedEventArgs e)
    {
        StringBuilder s = new();
        foreach (char c in TeamNumber.Text)
        {
            if (char.IsNumber(c)) s.Append(c);
        }
        if (s.Length != TeamNumber.Text.Length)
        {
            TeamNumber.Text = s.ToString();
        }
    }
}