using SQLite;

namespace BertScout2023.Models;

public class TeamMatch : BaseModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed(Name = "TeamMatchUnique", Order = 1, Unique = true)]
    public int TeamNumber { get; set; }

    [Indexed(Name = "TeamMatchUnique", Order = 2, Unique = true)]
    public int MatchNumber { get; set; }

    [Indexed(Unique = true)]
    public string Uuid { get; set; }

    public string AirtableId { get; set; }

    public bool Changed { get; set; }

    public bool Deleted { get; set; }

    public string ScoutName { get; set; }

    // autonomous

    public bool Auto_Mobility { get; set; }
    public bool Auto_Docked { get; set; }
    public bool Auto_Engaged { get; set; }

    public int Auto_Cubes_Top { get; set; }
    public int Auto_Cubes_Mid { get; set; }
    public int Auto_Cubes_Low { get; set; }
    public int Auto_Cones_Top { get; set; }
    public int Auto_Cones_Mid { get; set; }
    public int Auto_Cones_Low { get; set; }

    // teleop

    public int Tele_Cubes_Top { get; set; }
    public int Tele_Cubes_Mid { get; set; }
    public int Tele_Cubes_Low { get; set; }
    public int Tele_Cones_Top { get; set; }
    public int Tele_Cones_Mid { get; set; }
    public int Tele_Cones_Low { get; set; }

    // end game

    public bool Endgame_Parked { get; set; }
    public bool Endgame_Docked { get; set; }
    public bool Endgame_Engaged { get; set; }

    // overall

    public string Comments { get; set; }
}