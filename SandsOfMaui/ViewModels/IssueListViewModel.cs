namespace SandsOfMaui;

public partial class IssueListViewModel
{
    private CosmosService CloudService;
    private LocalDatabaseService DatabaseService;
    public ObservableCollection<Issue> ListOfIssues;

    public IssueListViewModel(CosmosService DICloudService, LocalDatabaseService DIDatabaseService)
    {
        CloudService = DICloudService;
        DatabaseService = DIDatabaseService;
    }

    public async Task<ObservableCollection<Issue>> FetchData()
    {
        Boolean IssueListInDB = Preferences.Get("IssueListInDB",false);

        if (!IssueListInDB)
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType == NetworkAccess.Internet)
            {
                ListOfIssues = await CloudService.FetchIssueList();   
                foreach (Issue issueToWrite in ListOfIssues)
                {
                    await DatabaseService.SaveIssueToDB(issueToWrite);
                }
                Preferences.Set("IssueListInDB", true);
            }
        }
        else
        {
            ListOfIssues = new ObservableCollection<Issue>(await DatabaseService.GetIssueListFromDB());
        }

        return ListOfIssues;
    }
}