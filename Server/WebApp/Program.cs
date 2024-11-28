using EfcRepositories;
using FileRepositories;
using RepositoryContracts;
using AppContext = EfcRepositories.AppContext;

namespace WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddScoped<IPostRepository, EfcPostRepository>(); // Old: PostFileRepository>();
        builder.Services.AddScoped<IUserRepository, EfcUserRepository>(); //Old: UserFileRepository>()
        builder.Services.AddScoped<ICommentRepository, EfcCommentRepository>(); // Old:CommentFileRepository>()
        builder.Services.AddDbContext<AppContext>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}