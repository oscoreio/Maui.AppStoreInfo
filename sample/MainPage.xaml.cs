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
}

