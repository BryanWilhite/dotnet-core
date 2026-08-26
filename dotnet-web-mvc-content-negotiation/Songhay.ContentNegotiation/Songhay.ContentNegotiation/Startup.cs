using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Songhay.ContentNegotiation.Models;
using Songhay.ContentNegotiation.OutputFormatters;

namespace Songhay.ContentNegotiation;

public class Startup(IConfiguration configuration)
{
    // This property is specified without explanation such as
    // in “Use Startup with the new minimal hosting model”
    // [ https://learn.microsoft.com/en-us/aspnet/core/migration/50-to-60 ]
    public IConfiguration Configuration { get; } = configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllersWithViews();

        services
            .AddSingleton<IContactRepository, ContactRepository>()
            .AddMvc(options =>
            {
                options.OutputFormatters.Add(new VcardOutputFormatter());
                //force vCard to be the default: options.OutputFormatters.Insert(0, new VcardOutputFormatter());
            });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app
                .UseExceptionHandler("/Home/Error")
                .UseHsts();
                // The default HSTS value is 30 days.
                // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        }

        app
            .UseHttpsRedirection()
            .UseStaticFiles()
            .UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
