using BertScout2023.Models;
using SQLite;

namespace BertScout2023.Databases;

public class LocalDatabase
{
    private const string DatabaseFilename = "bertscout2023.db3";

    private const SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLiteOpenFlags.SharedCache;

    private static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    private SQLiteAsyncConnection Database;

    public LocalDatabase()
    {
    }

    private async Task Init()
    {
        if (Database != null)
        {
            return;
        }
        Database = new(DatabasePath, Flags);
        await Database.CreateTableAsync<TeamMatch>();
    }

    public async Task<List<TeamMatch>> GetItemsAsync()
    {
        await Init();
        return await Database.Table<TeamMatch>()
            .ToListAsync();
    }

    public async Task<List<TeamMatch>> GetTeamAllMatches(int team)
    {
        await Init();
        return await Database.Table<TeamMatch>()
            .Where(i => i.TeamNumber == team)
            .OrderBy(i => i.MatchNumber)
            .ToListAsync();
    }

    public async Task<TeamMatch> GetTeamMatchAsync(int team, int match)
    {
        await Init();
        return await Database.Table<TeamMatch>()
            .Where(i => i.TeamNumber == team && i.MatchNumber == match)
            .FirstOrDefaultAsync();
    }

    public async Task<TeamMatch> GetItemAsync(int id)
    {
        await Init();
        return await Database.Table<TeamMatch>()
            .Where(i => i.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(TeamMatch item)
    {
        await Init();
        if (item.Id != 0)
        {
            return await Database.UpdateAsync(item);
        }
        item.Uuid = Guid.NewGuid().ToString();
        return await Database.InsertAsync(item);
    }
}