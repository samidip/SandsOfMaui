namespace SandsOfMaui;

public partial class TelerikUIView : ContentPage
{
	public TelerikUIView()
	{
		InitializeComponent();
	}

    void TelerikWebpageHTML_Navigated(System.Object sender, Microsoft.Maui.Controls.WebNavigatedEventArgs e)
    {
        this.TelerikWebpageFetchIndicator.IsBusy = false;
    }
}
