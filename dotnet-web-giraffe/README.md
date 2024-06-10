# .NET Core Giraffe Template

This sample shows the current [Giraffe](https://github.com/dustinmoris/Giraffe) template, installed from an elevated prompt:

```bash
dotnet new --install "giraffe-template"
```

Then we generate the project and the Solution file:

```bash
dotnet new giraffe -o Songhay.GiraffeOne/Songhay.GiraffeOne

dotnet new sln -n Songhay.GiraffeOne -o Songhay.GiraffeOne
dotnet sln Songhay.GiraffeOne/Songhay.GiraffeOne.sln add \
    Songhay.GiraffeOne/Songhay.GiraffeOne/Songhay.GiraffeOne.fsproj
```

Now it is very important to run this from directory of the Solution. So we build/run from the `Songhay.GiraffeOne` [directory](./Songhay.GiraffeOne):

```bash
dotnet build
dotnet run --project Songhay.GiraffeOne/Songhay.GiraffeOne.fsproj
```

## related links

* “[Functional ASP.NET Core part 2—Hello world from Giraffe](https://dusted.codes/functional-aspnet-core-part-2-hello-world-from-giraffe)”
* “[Getting Started with ASP.NET Core Giraffe](https://www.youtube.com/watch?v=HyRzsPZ0f0k&t=42s)”

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼
