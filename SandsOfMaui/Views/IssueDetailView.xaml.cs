namespace SandsOfMaui;

[QueryProperty(nameof(ChosenIssueID), "ChosenIssueID")]
public partial class IssueDetailView : ContentPage
{
	IssueViewModel MyViewModel;
	HTMLBuilder MyHTMLBuilder;
	private string chosenIssueID;

	public string ChosenIssueID
    {
        get => chosenIssueID;
        set { chosenIssueID = value; }
    }

	public IssueDetailView(IssueViewModel DIIssueViewModel, HTMLBuilder DIHTMLBuilder)
	{
		InitializeComponent();

		MyViewModel = DIIssueViewModel;
		MyHTMLBuilder = DIHTMLBuilder;
    }

    protected override async void OnAppearing()
	{
		base.OnAppearing();

		await FetchAndBindData(ChosenIssueID);
	}

	private async Task FetchAndBindData(string selectedIssueID)
	{
		await MyViewModel.FetchData(selectedIssueID);
		this.Title = "Sands of MAUI Issue # " + MyViewModel.SelectedIssue.ID;
        MyHTMLBuilder.BuildDOM(MyViewModel.SelectedIssue.HTMLBody);

		this.IssueHTML.Source = new HtmlWebViewSource
		{
			Html = MyHTMLBuilder.DOMToRender
        };
		this.IssueDetailFetchIndicator.IsBusy = false;
		MyHTMLBuilder.DOMToRender = string.Empty;
    }
}