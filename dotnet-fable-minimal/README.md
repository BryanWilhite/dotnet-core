# Fable on .NET Core

This sample is a snapshot of the `dotnet new` template for a “simple” Fable app.

From an elevated Powershell prompt we start with:

```ps1
dotnet new -i Fable.Template
```

Then we generate the project:

```ps1
dotnet new fable -o FableOne
```

>Warning: using a period in the `-o` option may produce unexpected results.

This command will generate a [README file](./FableOne/README.md) that supplements this one.

From the `FableOne` [folder](./FableOne), run:

```ps1
npm install
dotnet restore
```

From the `\FableOne\src` [folder](./FableOne/src), run:

```ps1
dotnet fable npm-start
```

You should see output like this:

```ps1
Fable (1.2.3) daemon started on port 61225
CWD: .\dotnet-fable-minimal\FableOne
cmd /C npm run start

> @ start .\dotnet-fable-minimal\FableOne
> webpack-dev-server

Bundling for development...
Project is running at http://localhost:8080/
webpack output is served from /
Content not from webpack is served from .\dotnet-fable-minimal\FableOne\public
Parsing ./FableOne.fsproj...
fable: Compiled src\FableOne.fsproj
fable: Compiled src\App.fs
Hash: 4794a15d5381b140bc19
Version: webpack 3.6.0
Time: 10890ms
        Asset    Size  Chunks                    Chunk Names
    bundle.js  328 kB       0  [emitted]  [big]  main
bundle.js.map  389 kB       0  [emitted]         main
  [35] multi (webpack)-dev-server/client?http://localhost:8080 ./src/FableOne.fsproj 40 bytes {0} [built]
  [36] (webpack)-dev-server/client?http://localhost:8080 7.27 kB {0} [built]
  [37] ./node_modules/url/url.js 23.3 kB {0} [built]
  [40] ./node_modules/querystring-es3/index.js 127 bytes {0} [built]
  [43] ./node_modules/strip-ansi/index.js 161 bytes {0} [built]
  [45] ./node_modules/loglevel/lib/loglevel.js 7.74 kB {0} [built]
  [46] (webpack)-dev-server/client/socket.js 1.04 kB {0} [built]
  [78] (webpack)-dev-server/client/overlay.js 3.71 kB {0} [built]
  [80] ./node_modules/html-entities/index.js 231 bytes {0} [built]
  [83] (webpack)/hot nonrecursive ^\.\/log$ 170 bytes {0} [built]
  [84] (webpack)/hot/log.js 1.04 kB {0} [optional] [built]
  [85] (webpack)/hot/emitter.js 77 bytes {0} [built]
  [86] ./node_modules/events/events.js 8.33 kB {0} [built]
  [87] ./src/FableOne.fsproj 25 bytes {0} [built]
  [88] ./src/App.fs 332 bytes {0} [built]
    + 74 hidden modules
webpack: Compiled successfully.
```

Finally, navigate to `http://localhost:8080/`.

## related links

* [Fable Issue #1166](https://github.com/fable-compiler/Fable/issues/1166)
* [Alfonso García-Caro—Fable](https://www.youtube.com/watch?v=K_r3p3l85uk)
* [Full Stack F# with Fable](https://channel9.msdn.com/events/dotnetConf/2017/T319)