namespace BertScout2023;

public partial class MainPage
{
    public void EnableTopRow(bool enable)
    {
        TeamNumber.IsEnabled = enable;
        MatchNumber.IsEnabled = enable;
        FormBody.IsVisible = !enable;
        Start.Text = enable ? "Start" : "Save";
        TeamNumber.TextColor = enable ? Colors.Black : Colors.Gray;
        MatchNumber.TextColor = enable ? Colors.Black : Colors.Gray;
    }
    public bool ValidateTeamNumber(string teamNumber)
    {
        if (!int.TryParse(teamNumber, out int tNumber)) { return false; 
        }
        if (tNumber < 1) { return false; 
        }
        return true;
    }
    public bool ValidateMatchNumber(string matchNumber)
    {
        if (!int.TryParse(matchNumber, out int mNumber))
        {
            return false;
        }
        if (mNumber < 1)
        {
            return false;
        }
        return true;
    }
}