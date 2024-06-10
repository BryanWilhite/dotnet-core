# Fable

<https://fable.io/>

>Fable is a compiler that brings F# into the JavaScript ecosystem…

## setup

```shell
dotnet new --install Fable.Template
```

From the [`dotnet-web-fable/`](../dotnet-web-fable) directory:

```shell
dotnet new fable -n Fable.One -o Fable.One
```

As of this writing the Fable template shipped with `webpack` 4.x. To avoid vulnerabilities upgrade to 5.x with:

```shell
npm audit fix --force
```

This will cause a breaking change: the `devServer` property in `webpack.config.js` [file](./Fable.One/webpack.config.js) needs to change from this:

```json
devServer: {
    publicPath: "/",
    contentBase: "./public",
    port: 8080,
},
```

to this:

```json
devServer: {
    port: 8080,
    static: {
        directory: path.join(__dirname, "./public"),
        publicPath: "/",
    }
},
```

For more detail, see the `webpack` [docs](https://webpack.js.org/configuration/dev-server/#devserver).

🐙🐱[BryanWilhite](https://github.com/BryanWilhite)
