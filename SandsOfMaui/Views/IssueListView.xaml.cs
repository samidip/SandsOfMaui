namespace SandsOfMaui;

public partial class IssueListView : ContentPage
{
	IssueListViewModel MyViewModel;

	public IssueListView(IssueListViewModel DIIssueListViewModel)
	{
		InitializeComponent();

		MyViewModel = DIIssueListViewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		await FetchAndBindData();
	}

	private async Task FetchAndBindData()
	{
		await MyViewModel.FetchData();		
		this.IssueListFetchIndicator.IsBusy = false;
		this.SandsofMauiIssueList.ItemsSource = MyViewModel.ListOfIssues;

		if (MyViewModel.ListOfIssues.Count == 0)
		{
			this.IssueListFetchIndicator.IsBusy = false;
			await DisplayAlert("Network Alert!", "Your device likely does not have connectivity - please try again later.", "OK");
			return;
		}

		Boolean AppLaunchedFromNotification = Preferences.Get("AppLaunchedFromNotification",false);
		if (AppLaunchedFromNotification)
		{
			this.SandsofMauiIssueList.ScrollItemIntoView(MyViewModel.ListOfIssues[MyViewModel.ListOfIssues.Count - 1]);
			Preferences.Set("AppLaunchedFromNotification", false);
		}
	}

	private async void IssuePicked(System.Object sender, Telerik.Maui.Controls.Compatibility.DataControls.ListView.ItemTapEventArgs e)
	{
		Issue selectedIssue = (Issue)e.Item;
		// await Navigation.PushAsync(new IssueDetailView(selectedIssue.ID, DetailViewModel));
		await Shell.Current.GoToAsync($"IssueDetail?ChosenIssueID={selectedIssue.ID}");
	}
}