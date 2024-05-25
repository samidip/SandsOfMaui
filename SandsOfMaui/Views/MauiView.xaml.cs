namespace SandsOfMaui;

public partial class MauiView : ContentPage
{
	public MauiView()
	{
		InitializeComponent();
	}

    void MauiWebpageHTML_Navigated(System.Object sender, Microsoft.Maui.Controls.WebNavigatedEventArgs e)
    {
        this.MauiWebpageFetchIndicator.IsBusy = false;
    }
}
