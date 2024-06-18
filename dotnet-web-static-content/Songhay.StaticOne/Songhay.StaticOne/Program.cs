WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapGet("/", async context =>
    {
        string inlineHtml = @"<a href=""./static_file.html"">Hello World!</a>";
        await context.Response.WriteAsync(inlineHtml);
    });

app.UseStaticFiles();

app.Run();
