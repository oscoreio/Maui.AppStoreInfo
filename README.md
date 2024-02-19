# Maui.InAppUpdates

[![Nuget package](https://img.shields.io/nuget/vpre/Oscore.Maui.InAppUpdates)](https://www.nuget.org/packages/Oscore.Maui.InAppUpdates/)
[![CI/CD](https://github.com/oscoreio/Maui.InAppUpdates/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/oscoreio/Maui.InAppUpdates/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/github/license/oscoreio/Maui.InAppUpdates)](https://github.com/oscoreio/Maui.InAppUpdates/blob/main/LICENSE)

Allows you to monitor the availability of updates in App stores and suggest actions to the user based on this

# Usage
- Add NuGet package to your project:
```xml
<PackageReference Include="Oscore.Maui.InAppUpdates" Version="1.0.0" />
```
- Add the following to your `MauiProgram.cs` `CreateMauiApp` method:
```diff
builder
    .UseMauiApp<App>()
+   .UseInAppUpdates()
    .ConfigureFonts(fonts =>
    {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
    });
```

# Links
- https://github.com/edsnider/latestversionplugin/
- https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/appmodel/app-information?view=net-maui-8.0&tabs=android
- https://stackoverflow.com/questions/49072305/official-api-for-grabbing-app-version-on-google-playstore/58590547#58590547
- https://stackoverflow.com/questions/60043944/does-ios-has-in-app-updates-like-feature-as-of-android
