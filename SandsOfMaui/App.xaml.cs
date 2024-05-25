namespace SandsOfMaui;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		this.MainPage = new AppShell();
		
		this.ScheduleNotifications();
		LocalNotificationCenter.Current.NotificationActionTapped += OnNotificationActionTapped;
	}

	private async void ScheduleNotifications()
	{
		if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
		{
			await LocalNotificationCenter.Current.RequestNotificationPermission();
		}

		var publicationTime = new TimeSpan(12,0,0);
		var today = DateTime.Today;
		var daysUntilMonday = ((int) DayOfWeek.Monday - (int) today.DayOfWeek + 7) % 7;
		var nextNotification = today.AddDays(daysUntilMonday) + publicationTime;		

		var notification = new NotificationRequest
		{
			NotificationId = new Random().Next(),
			Title = "New Grain of MAUI Sands",
			Description = "Heyo. It's another Monday - time to check out the latest Sands of MAUI publication!",
			Schedule = 
			{	
				NotifyTime = nextNotification,
				// NotifyTime = DateTime.Now.AddSeconds(15),
				RepeatType = NotificationRepeat.Weekly 
			}
		};
		await LocalNotificationCenter.Current.Show(notification);
	}

	private void OnNotificationActionTapped(NotificationActionEventArgs e)
	{
		Preferences.Set("IssueListInDB", false);
		Preferences.Set("AppLaunchedFromNotification", true);
	}
}
