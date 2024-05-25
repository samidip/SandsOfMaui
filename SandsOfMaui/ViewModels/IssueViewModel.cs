namespace SandsOfMaui;

public partial class IssueViewModel
{
    private CosmosService CloudService;
    private LocalDatabaseService DatabaseService;
    public IssueDetail SelectedIssue = new IssueDetail();

    public async Task<IssueDetail> FetchData(string selectedIssueID)
    {
        string IssueIdentifer = "Issue#" + selectedIssueID;
        Boolean IssueDetailInDB = Preferences.Get(IssueIdentifer,false);
        DatabaseService = new LocalDatabaseService();

        if (!IssueDetailInDB)
        {
            CloudService = new CosmosService();
		    SelectedIssue = await CloudService.FetchIssue(selectedIssueID);
            
            await DatabaseService.SaveIssueDetailToDB(SelectedIssue);
            Preferences.Set(IssueIdentifer, true);
        }
        else
        {
            SelectedIssue  = await DatabaseService.GetIssueDetailFromDB(selectedIssueID);
        }

        return SelectedIssue;
    }
}