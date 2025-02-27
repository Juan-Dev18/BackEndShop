using Microsoft.Extensions.DependencyInjection;
using WebApiShop.Data;
using WebApiShop.Interface;
using WebApiShop.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


builder.Services.Configure<ClsConnection>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddSingleton<IOrder, OrderRepository>();
builder.Services.AddSingleton<ICustomer, CustomerRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name : "CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGraphQL();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
