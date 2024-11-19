namespace SandsOfMaui;

public partial class TelerikUIView : ContentPage
{
	public TelerikUIView()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
	{
		base.OnAppearing();

        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            this.TelerikWebpageHTML.Source = "https://www.telerik.com/maui-ui";
        }
        else
        {
            await DisplayAlert("Network Alert!", "Your device likely does not have connectivity - please try again later.", "OK");
            return;
        }
	}

    void TelerikWebpageHTML_Navigated(System.Object sender, Microsoft.Maui.Controls.WebNavigatedEventArgs e)
    {
        this.TelerikWebpageFetchIndicator.IsBusy = false;
    }
}
