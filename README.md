# dotnet-core

Here’s my collection of self-educational samples on .NET Core. The intention here is to address these concerns:

* .NET scripting in C# and F#.
* Using F# as alternative to TypeScript with [Fable](http://fable.io/).
* Migrating to Entity Framework Core.
* Migrating ASP.NET Web API to ASP.NET Core.

The bias here is to run everything in bash without regard to platform. “[Running .NET Core Apps under Windows Subsystem for Linux (Bash for Windows)](https://weblog.west-wind.com/posts/2017/Apr/13/Running-NET-Core-Apps-under-Windows-Subsystem-for-Linux-Bash-for-Windows)” by Rick Strahl is a great introduction to this possibility.

When bash fails then the alternative will be PowerShell. _Visual Studio Code users_: install the [PowerShell](https://marketplace.visualstudio.com/items?itemName=ms-vscode.PowerShell) extension to get the **PowerShell: Show Integrated Console** command to run PowerShell and bash sessions on top of each other in Visual Studio Code.

## setup on Ubuntu bash

This is a summary of shell commands for [setting up `dotnet` on Ubuntu](https://www.microsoft.com/net/core#linuxubuntu):

```bash

curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
sudo mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg

sudo sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-xenial-prod xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
sudo apt-get update

sudo apt-get install dotnet-sdk-2.0.0

```

## what does Node.js have to do with .NET Core?

There _is_ a relationship between Node.js and .NET Core. The work on [Fable](http://fable.io/) that will done here will demonstrate this. It follows that [my study of Node.js](https://github.com/BryanWilhite/nodejs) is recommended reading to background the word here.
