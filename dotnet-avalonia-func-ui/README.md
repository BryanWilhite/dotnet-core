# Avalonia FuncUI

## <https://funcui.avaloniaui.net/>

We see in our Avalonia [samples](../dotnet-avalonia), a pure XAML-based approach. Avalonia FuncUI allows us to approach desktop UI development with a “code first” approach.

The following is based on the [Getting Started](https://funcui.avaloniaui.net/) docs:

From the `/dotnet-avalonia-func-ui` [directory](../dotnet-avalonia-func-ui):

```bash
dotnet new console -o NewApp -lang F#
cd NewApp/
dotnet add package Avalonia.Desktop --version 11.0.0
dotnet add package Avalonia.Themes.Fluent --version 11.0.0
dotnet add package Avalonia.FuncUI --version 1.0.0
```

Change the `dotnet-avalonia-func-ui/NewApp/Program.fs` [file](../dotnet-avalonia-func-ui/NewApp/Program.fs) to:

```fsharp
namespace CounterApp

open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Themes.Fluent
open Avalonia.FuncUI.Hosts
open Avalonia.Controls
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Layout

module Main =

    let view () =
        Component(fun ctx ->
            let state = ctx.useState 0

            DockPanel.create [
                DockPanel.children [
                    Button.create [
                        Button.dock Dock.Bottom
                        Button.onClick (fun _ -> state.Set(state.Current - 1))
                        Button.content "-"
                        Button.horizontalAlignment HorizontalAlignment.Stretch
                        Button.horizontalContentAlignment HorizontalAlignment.Center
                    ]
                    Button.create [
                        Button.dock Dock.Bottom
                        Button.onClick (fun _ -> state.Set(state.Current + 1))
                        Button.content "+"
                        Button.horizontalAlignment HorizontalAlignment.Stretch
                        Button.horizontalContentAlignment HorizontalAlignment.Center
                    ]
                    TextBlock.create [
                        TextBlock.dock Dock.Top
                        TextBlock.fontSize 48.0
                        TextBlock.verticalAlignment VerticalAlignment.Center
                        TextBlock.horizontalAlignment HorizontalAlignment.Center
                        TextBlock.text (string state.Current)
                    ]
                ]
            ]
        )

type MainWindow() =
    inherit HostWindow()
    do
        base.Title <- "Counter Example"
        base.Content <- Main.view ()

type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Add (FluentTheme())
        this.RequestedThemeVariant <- Styling.ThemeVariant.Dark

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
            desktopLifetime.MainWindow <- MainWindow()
        | _ -> ()

module Program =

    [<EntryPoint>]
    let main(args: string[]) =
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .UseSkia()
            .StartWithClassicDesktopLifetime(args)
```

From the `/dotnet-avalonia-func-ui` [directory](../dotnet-avalonia-func-ui):

```bash
dotnet run
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼
