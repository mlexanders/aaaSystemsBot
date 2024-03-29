using aaaSystemsApi;
using aaaSystemsApi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultSqlite"));
});

//DI
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<RoomRepository>();
builder.Services.AddTransient<RoomMessageRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

using (var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>())
{
    dbContext.Database.Migrate();
}

app.Run();
