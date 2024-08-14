using Shopper_Demo.Repositories;
using Shopper_Demo.Interfaces;
using Microsoft.OpenApi.Models;

// using Shopper_Demo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopper Demo API", Version = "v1" });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSingleton<IInMemoryInventory, InMemoryInventory>();
builder.Services.AddSingleton<IInMemoryOrder, InMemoryOrder>();
builder.Services.AddSingleton<IInMemoryShopper, InMemoryShopper>();


// add dependency injection so inmemrepo can access the dictionary from inmeminventory
builder.Services.AddSingleton<IInMemoryRepository>(sp =>
{
    var inventory = sp.GetRequiredService<IInMemoryInventory>();
    return new InMemoryRepository(inventory.ProductIdMap);
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopper Demo API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
