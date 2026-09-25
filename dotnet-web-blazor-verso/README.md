# Verso Notebooks

>Microsoft deprecated Polyglot Notebooks on February 11, 2026, and .NET Interactive, the engine that powered it, followed the same path. Together they were the primary way to run interactive C#, F#, PowerShell, and SQL in a notebook. Their deprecation left a gap in the .NET ecosystem: no maintained notebook platform, and no maintained embeddable execution engine.
> 
> Verso fills both roles.
> 
> **As a notebook platform**, Verso runs in VS Code or any browser, ships with IntelliSense and variable sharing across every language it supports, and imports existing `.ipynb` and `.dib` files. If you used Polyglot Notebooks, the experience will feel familiar.
> 
> **As an engine**, the core is a headless .NET library with no UI dependencies. It provides multi-language execution, an extension host, a variable store, and a layout manager through a clean set of public interfaces. If you embedded .NET Interactive in a tool, service, or workflow, the Verso engine serves the same purpose with a fully extensible architecture. Reference the NuGet package, wire up a `Scaffold`, and you have a programmable notebook runtime in any .NET application.
> 
> The architecture is built on one principle: every feature is an extension, and every extension uses the same public interfaces available to anyone. The C# kernel, the dark theme, and the dashboard layout all ship as extensions with no special access to engine internals. If a built-in feature needs an internal API to work, the interfaces are incomplete.
>
>—<https://github.com/DataficationSDK/Verso>
>

## migrating my old Polyglot Notebooks over here to Verso

Most of the .NET-based Jupyter Notebooks under my `funkykb` directory [[GitHub](https://github.com/BryanWilhite/jupyter-central/tree/main/funkykb)] should start appearing in this `dotnet-web-blazor-verso` [directory](../dotnet-web-blazor-verso). Since verso does not support static display in the browser at this time, I will probably use a bunch of Markdown files, exported from Verso.

To get started, I will need to install the verso CLI:

```bash
dotnet tool install -g Verso.Cli
```

Now, to convert `*.ipynb` files, I run the `verso convert` command:

```bash
verso convert notebook.ipynb --to verso --output cleaned.verso --strip-outputs
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼
