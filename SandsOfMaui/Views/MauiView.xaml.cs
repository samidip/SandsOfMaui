namespace SandsOfMaui;

public partial class MauiView : ContentPage
{
	public MauiView()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
	{
		base.OnAppearing();

        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            this.MauiWebpageHTML.Source = "https://dotnet.microsoft.com/en-us/apps/maui";
        }
        else
        {
            await DisplayAlert("Network Alert!", "Your device likely does not have connectivity - please try again later.", "OK");
            return;
        }
	}

    void MauiWebpageHTML_Navigated(System.Object sender, Microsoft.Maui.Controls.WebNavigatedEventArgs e)
    {
        this.MauiWebpageFetchIndicator.IsBusy = false;
    }
}
