namespace SandsOfMaui;

public partial class LocalDatabaseService
{
	public const string SandsofMauiDBFile = "SandsofMauiSQLite.db3";
    public static string SandsofMauiDBPath => Path.Combine(FileSystem.AppDataDirectory, SandsofMauiDBFile);
    public const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.ReadWrite;

    SQLiteAsyncConnection SandsofMauiDBConnection;

    async Task DBInit()
    {
        if (SandsofMauiDBConnection is not null)
            return;

        SandsofMauiDBConnection = new SQLiteAsyncConnection(SandsofMauiDBPath, Flags);
        var IssueListTable = await SandsofMauiDBConnection.CreateTableAsync<Issue>();
        var IssueDetailTable = await SandsofMauiDBConnection.CreateTableAsync<IssueDetail>();
    }

    public async Task<List<Issue>> GetIssueListFromDB()
    {
        await DBInit();
        return await SandsofMauiDBConnection.Table<Issue>().ToListAsync();
    }

    public async Task<IssueDetail> GetIssueDetailFromDB(string issueID)
    {
        await DBInit();
        return await SandsofMauiDBConnection.Table<IssueDetail>().Where(i => i.ID == issueID).FirstOrDefaultAsync();
    }

    public async Task<int> SaveIssueToDB(Issue issueToInsert)
    {
        await DBInit();
        return await SandsofMauiDBConnection.InsertAsync(issueToInsert);
    }

    public async Task<int> SaveIssueDetailToDB(IssueDetail issueToInsert)
    {
        await DBInit();
        return await SandsofMauiDBConnection.InsertAsync(issueToInsert);
    }
}


