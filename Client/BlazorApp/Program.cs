using System;
using System.Net.Http;
using BlazorApp.Components;
using BlazorApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazorApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        
       builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5250") });
      // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7180") });
        builder.Services.AddScoped<IUserService, HttpUserService>();
        builder.Services.AddScoped<IPostService, HttpPostService>();
        builder.Services.AddScoped<ICommentService, HttpCommentService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}