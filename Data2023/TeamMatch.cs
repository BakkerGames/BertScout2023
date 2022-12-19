using SQLite;

namespace Data2023;

// IMPORTANT! Increment DatabaseServices.dbVersion whenever this changes!

public class TeamMatch
{
    // metadata
    [PrimaryKey, AutoIncrement]
    public int? Id { get; set; }
    public string Uuid { get; set; } = "";
    public string AirtableId { get; set; } = "";
    public bool Changed { get; set; }
    public bool Deleted { get; set; }

    [Indexed]
    public int TeamNumber { get; set; }
    [Indexed]
    public int MatchNumber { get; set; }
    public string ScouterName { get; set; } = "";

    // autonomous

    // teleop

    // end game

    // overall
    public string Comments { get; set; } = "";
}