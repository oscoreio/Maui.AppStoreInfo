# Maui.AppStoreInfo

[![Nuget package](https://img.shields.io/nuget/vpre/Oscore.Maui.AppStoreInfo)](https://www.nuget.org/packages/Oscore.Maui.AppStoreInfo/)
[![CI/CD](https://github.com/oscoreio/Maui.AppStoreInfo/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/oscoreio/Maui.AppStoreInfo/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/github/license/oscoreio/Maui.AppStoreInfo)](https://github.com/oscoreio/Maui.AppStoreInfo/blob/main/LICENSE)

Allows you to check the information in App stores(for example the latest published version)
and suggest actions to the user based on this.

# Usage
- Add NuGet package to your project:
```xml
<PackageReference Include="Oscore.Maui.AppStoreInfo" Version="1.0.0" />
```
- Add the following to your `MauiProgram.cs` `CreateMauiApp` method:
```diff
builder
    .UseMauiApp<App>()
+   .UseAppStoreInfo(options =>
+   {
+       options.CountryCode = "gb"; // Optional, default is "us"
+   })
    .ConfigureFonts(fonts =>
    {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
    });
```
- Use the `AppStoreInfo.Current` class or `IAppStoreInfo` from DI to check the latest version and suggest actions to the user:
```csharp
if (!await AppStoreInfo.Current.IsUsingLatestVersionAsync())
{
    await AppStoreInfo.Current.OpenApplicationInStoreAsync();
}
```

# Notes
- Since Android doesn't provide an official API, there is no support for this other than opening a store page. It is recommended to use [Android In-App Updates](https://github.com/oscoreio/Maui.Android.InAppUpdates) if you need to check for updates.

# Links
- https://github.com/edsnider/latestversionplugin/
- https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/appmodel/app-information?view=net-maui-8.0&tabs=android
- https://stackoverflow.com/questions/49072305/official-api-for-grabbing-app-version-on-google-playstore/58590547#58590547
- https://stackoverflow.com/questions/60043944/does-ios-has-in-app-updates-like-feature-as-of-android
