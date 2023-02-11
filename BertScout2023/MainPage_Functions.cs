using BertScout2023.Models;

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

    public static bool ValidateTeamNumber(string teamNumber)
    {
        if (!int.TryParse(teamNumber, out int tNumber))
        {
            return false;
        }
        if (tNumber < 1)
        {
            return false;
        }
        return true;
    }

    public static bool ValidateMatchNumber(string matchNumber)
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

    public static bool ValidateScoutName(string scoutName)
    {
        if (string.IsNullOrWhiteSpace(scoutName))
        {
            return false;
        }
        return true;
    }

    public void ClearAllFields()
    {
        Comments.Text = "";
    }

    private void FillFields(TeamMatch item)
    {
        LabelAutoCubeTop.Text = item.Auto_Cubes_Top.ToString();
        LabelAutoCubeMid.Text = item.Auto_Cubes_Mid.ToString();
        LabelAutoCubeLow.Text = item.Auto_Cubes_Low.ToString();
        LabelAutoConeTop.Text = item.Auto_Cones_Top.ToString();
        LabelAutoConeMid.Text = item.Auto_Cones_Mid.ToString();
        LabelAutoConeLow.Text = item.Auto_Cones_Low.ToString();
        ButtonAutoMobility.BackgroundColor = item.Auto_Mobility ? Colors.Green : Colors.Gray;
        ButtonAutoDocked.BackgroundColor = item.Auto_Docked ? Colors.Green : Colors.Gray;
        ButtonAutoEngaged.BackgroundColor = item.Auto_Engaged ? Colors.Green : Colors.Gray;
        LabelTeleCubeTop.Text = item.Tele_Cubes_Top.ToString();
        LabelTeleCubeMid.Text = item.Tele_Cubes_Mid.ToString();
        LabelTeleCubeLow.Text = item.Tele_Cubes_Low.ToString();
        LabelTeleConeTop.Text = item.Tele_Cones_Top.ToString();
        LabelTeleConeMid.Text = item.Tele_Cones_Mid.ToString();
        LabelTeleConeLow.Text = item.Tele_Cones_Low.ToString();
        ButtonEndgameParked.BackgroundColor = item.Endgame_Parked ? Colors.Green : Colors.Gray;
        ButtonEndgameDocked.BackgroundColor = item.Endgame_Docked ? Colors.Green : Colors.Gray;
        ButtonEndgameEngaged.BackgroundColor = item.Endgame_Engaged ? Colors.Green : Colors.Gray;
        Comments.Text = item.Comments;
        CommentPicker.SelectedIndex = -1;
    }

    private void StoreFields(TeamMatch item)
    {
        if (string.IsNullOrWhiteSpace(item.ScoutName))
            item.ScoutName = ScoutName.Text;
        // everything else handled by Clicked/Changed events
    }
}