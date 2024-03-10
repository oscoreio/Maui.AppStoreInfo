using CommunityToolkit.Mvvm.Input;

namespace Maui.AppStores.SampleApp;

public partial class MainPage : ContentPage
{
	public MainPage()
    {
        InitializeComponent();
        
        BindingContext = this;
    }

	[RelayCommand]
	private async Task OpenStore()
	{
		try
		{
			await AppStoreInfo.Current.OpenApplicationInStoreAsync();
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
	}

	[RelayCommand]
	private async Task CheckLatestVersion()
	{
		try
		{
			if (!await AppStoreInfo.Current.IsUsingLatestVersionAsync())
			{
				await AppStoreInfo.Current.OpenApplicationInStoreAsync();
			}
			else
			{
				await DisplayAlert("Latest Version", "You are using the latest version of the app.", "OK");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
	}
	
	[RelayCommand]
	private async Task RequestAppStoreInformation()
	{
		try
		{
			var information = await AppStoreInfo.Current.GetInformationAsync();
            
			await DisplayAlert(
				"App Store Information",
				$"Title: {information.Title}\n" +
				$"Description: {information.Description}\n" +
				$"Latest Version: {information.LatestVersion}\n" +
				$"External Store Uri: {information.ExternalStoreUri}\n" +
				$"Internal Store Uri: {information.InternalStoreUri}\n" +
				$"Internal Review Uri: {information.InternalReviewUri}\n" +
				$"Release Notes: {information.ReleaseNotes}\n" +
				$"Application Size: {information.ApplicationSizeInBytes/1024/1024} MB\n",
				"OK");
		}
		catch (Exception e)
		{
			await DisplayAlert("Error", e.Message, "OK");
		}
	}

	[RelayCommand]
	private async Task OpenStoreReviewPage()
	{
		try
		{
			await AppStoreInfo.Current.OpenStoreReviewPage();
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
	}
}

