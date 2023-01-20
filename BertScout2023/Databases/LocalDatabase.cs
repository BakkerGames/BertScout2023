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
        var basePath = FileSystem.AppDataDirectory;
        var databasePath = Path.Combine(basePath, DatabaseFilename);
        try
        {
            Database = new(databasePath, Flags);
            await Database.CreateTableAsync<TeamMatch>();
        }
        catch (Exception ex)
        {
            throw new SystemException($"Error initializing database: {databasePath}\r\n{ex.Message}");
        }
    }

    public async Task<List<TeamMatch>> GetItemsAsync()
    {
        await Init();
        return await Database.Table<TeamMatch>()
            .ToListAsync();
    }

    public async Task<List<TeamMatch>> GetChangedItemsAsync()
    {
        await Init();
        return await Database.Table<TeamMatch>()
            .Where(x => x.Changed)
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
        var oldItem = await GetTeamMatchAsync(item.TeamNumber, item.MatchNumber);
        if (oldItem != null)
        {
            item.Id = oldItem.Id;
            item.Uuid = oldItem.Uuid;
            item.AirtableId = oldItem.AirtableId;
            return await Database.UpdateAsync(item);
        }
        item.Uuid = Guid.NewGuid().ToString();
        return await Database.InsertAsync(item);
    }
}