namespace SandsOfMaui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("IssueDetail", typeof(IssueDetailView));
	}
}

