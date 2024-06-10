# Avalonia UI

## <https://www.avaloniaui.net/>

>…a cross-platform .NET framework for building beautiful, modern graphical user interfaces (GUIs). With Avalonia, you can create native applications for Windows, macOS, Linux, iOS, Android, and WebAssembly, all from a single codebase.

## installation

```shell
dotnet new install Avalonia.Templates
```

The following templates should be added:

| Template name | short name | language(s) | tags |
| - | - | - | - |
| Avalonia .NET Core App | avalonia.app | [C#],F# | Desktop/Xaml/Avalonia/|Windows/Linux/macOS |
| Avalonia .NET Core MVVM App | avalonia.mvvm | [C#],F# | Desktop/Xaml/Avalonia/Windows/Linux/macOS |
| Avalonia Cross Platform Application | avalonia.xplat | [C#],F# | Desktop/Xaml/Avalonia/Web/Mobile |
| Avalonia Resource Dictionary | avalonia.resource | | Desktop/Xaml/Avalonia/Windows/Linux/macOS |
| Avalonia Styles | avalonia.styles | | Desktop/Xaml/Avalonia/Windows/Linux/macOS |
| Avalonia TemplatedControl | avalonia.templatedcontrol | [C#],F# | Desktop/Xaml/Avalonia/Windows/Linux/macOS |
| Avalonia UserControl | avalonia.usercontrol | [C#],F# | Desktop/Xaml/Avalonia/Windows/Linux/macOS |
| Avalonia Window | avalonia.window | [C#],F# | Desktop/Xaml/Avalonia/Windows/Linux/macOS |

## generating and running `avalonia.app`

From the `dotnet-avalonia` [folder](../dotnet-avalonia):

```shell
dotnet new avalonia.app -o MyApp
cd MyApp
dotnet run
```

## generating and running `avalonia.mvvm`

From the `dotnet-avalonia` [folder](../dotnet-avalonia):

```shell
dotnet new avalonia.mvvm -o MyMvvmApp
cd MyMvvmApp
dotnet run
```

## generating and running `avalonia.mvvm` (for F#)

From the `dotnet-avalonia` [folder](../dotnet-avalonia):

```shell
dotnet new avalonia.mvvm -o MyFSharpMvvmApp -lang F#
cd MyFSharpMvvmApp
dotnet run
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼
