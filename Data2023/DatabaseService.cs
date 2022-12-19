using SQLite;
using System.Text;

namespace Data2023;

public class DatabaseService
{
    // update when db structure (TeamMatch) changes
    public const decimal dbVersion = 0.1M;

    public const string dbFilename = "bertscout2023.db3";

    private static SQLiteAsyncConnection _database;

    public DatabaseService(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.FullMutex);
        CreateTables();
    }

    public static async void CreateTables()
    {
        await _database.CreateTableAsync<TeamMatch>();
    }

    public static async void DropTables()
    {
        await _database.DropTableAsync<TeamMatch>();
    }

    public static async void ClearTables()
    {
        await _database.ExecuteAsync("TRUNCATE TABLE [TeamMatch];");
    }

    // TeamMatch

    public static async Task<List<TeamMatch>> GetTeamMatchesAsync()
    {
        return await _database.Table<TeamMatch>().ToListAsync();
    }

    public static async Task<TeamMatch?> GetTeamMatchAsync(int teamNumber, int matchNumber)
    {
        StringBuilder query = new();
        query.Append("SELECT [TeamMatch].* FROM [TeamMatch]");
        query.Append(" WHERE [TeamMatch].[TeamNumber] = ");
        query.Append(teamNumber);
        query.Append(" AND [TeamMatch].[MatchNumber] = ");
        query.Append(matchNumber);
        List<TeamMatch> result = await _database.QueryAsync<TeamMatch>(query.ToString());
        if (result == null || result.Count == 0)
        {
            return null;
        }
        return result[0];
    }

    public static async Task<int> SaveTeamMatchAsync(TeamMatch item)
    {
        if (item.Uuid == null)
        {
            TeamMatch? existingItem = await GetTeamMatchAsync(item.TeamNumber, item.MatchNumber);
            if (existingItem != null)
            {
                // this item is bad, probably doesn't have a UUID
                await _database.DeleteAsync(existingItem);
            }
            item.Uuid = Guid.NewGuid().ToString();
        }
        return await _database.InsertOrReplaceAsync(item);
    }

    public static async Task<int> ActualDeleteMatchAsync(TeamMatch item)
    {
        if (item.Uuid == null)
        {
            return 0;
        }
        return await _database.DeleteAsync(item);
    }
}