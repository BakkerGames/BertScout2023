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
        ScoutName.Text = item.ScoutName;
        // todo fill all other fields
        Comments.Text = item.Comments;
        CommentPicker.SelectedIndex = -1;
    }

    private void StoreFields(TeamMatch item)
    {
        item.ScoutName = ScoutName.Text;
        // todo store all other fields
        item.Comments = Comments.Text;
    }
}