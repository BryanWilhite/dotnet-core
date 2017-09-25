# dotnet-core

my collection of self-educational samples on .NET Core

## setup on Ubuntu bash

This is a summary of shell commands for [setting up `dotnet` on Ubuntu](https://www.microsoft.com/net/core#linuxubuntu):

```bash

curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
sudo mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg

sudo sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-xenial-prod xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
sudo apt-get update

sudo apt-get install dotnet-sdk-2.0.0

```
