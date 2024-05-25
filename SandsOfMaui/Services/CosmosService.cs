namespace SandsOfMaui;

public partial class CosmosService
{
    private static readonly string EndpointUri = "Your Cosmos DB Endpoint URI";
    private static readonly string PrimaryKey = "Your Cosmos DB Key";
    
    private CosmosClient cosmosClient;
    private Database database;
    private Container container;

    private string issueListDatabaseId = "sandsofmauiissuelist";
    private string issueListContainerId = "issuelist";
    private string issueDatabaseId = "sandsofmauiissues";
    private string issueContainerId = "issues";
    private ObservableCollection<Issue> ListOfIssues = new ObservableCollection<Issue>();
    private IssueDetail SelectedIssue = new IssueDetail();

    public async Task<ObservableCollection<Issue>> FetchIssueList()
    {
        this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
        this.database = cosmosClient.GetDatabase(issueListDatabaseId);
        this.container = cosmosClient.GetContainer(issueListDatabaseId,issueListContainerId);
        await this.QueryItemsAsync();

        return ListOfIssues;
    }

    public async Task<IssueDetail> FetchIssue(string ID)
    {
        this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
        this.database = cosmosClient.GetDatabase(issueDatabaseId);
        this.container = cosmosClient.GetContainer(issueDatabaseId,issueContainerId);
        await this.QueryItemAsync(ID);

        return SelectedIssue;
    }

    private async Task QueryItemsAsync()
    {
       
        var sqlQueryText = "SELECT * FROM c";

        QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
        FeedIterator<Issue> queryResultSetIterator = this.container.GetItemQueryIterator<Issue>(queryDefinition);

        try
        {
            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Issue> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Issue issue in currentResultSet)
                {
                    ListOfIssues.Add(issue);
                }
            }
        }
        catch(Exception ex) { }
    }

    private async Task QueryItemAsync(string ID)
    {
        var sqlQueryText = "SELECT * FROM c WHERE c.id=" + "'" + ID + "'";

        QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
        FeedIterator<IssueDetail> queryResultSetIterator = this.container.GetItemQueryIterator<IssueDetail>(queryDefinition);
        
        while (queryResultSetIterator.HasMoreResults)
        {
            FeedResponse<IssueDetail> currentResultSet = await queryResultSetIterator.ReadNextAsync();
            foreach (IssueDetail issue in currentResultSet)
            {
                SelectedIssue = issue;
            }
        }
    }
}