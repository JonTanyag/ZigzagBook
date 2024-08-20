using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Zigzag.Api;
using Zigzag.Api.Filters;
using Zigzag.Application;
using Zigzag.Application.Common.Helper;
using Zigzag.Core;
using Zigzag.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Mediator
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(AddBookCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(UpdateBookCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(DeleteBookCommand).GetTypeInfo().Assembly);


builder.Services.AddMediatR(typeof(GetBookByIdQuery).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetBooksQuery).GetTypeInfo().Assembly);


// DbContext
builder.Services.AddDbContext<BookDbContext>(options =>
                options.UseInMemoryDatabase("BookDb"));

// Services
builder.Services.AddTransient<IAddBookService, AddBookService>();
builder.Services.AddTransient<IUpdateBookService, UpdateBookService>();
builder.Services.AddTransient<IDeleteBookService, DeleteBookService>();
builder.Services.AddTransient<IGetBookService, GetBookService>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IdGenerator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library API",
        Description = "A simple API for managing a library system.",
        Version = "v1"
    });
    c.EnableAnnotations();

    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "X-Api-Token",
        In = ParameterLocation.Header,
        Description = "API token needed to access the endpoints"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                In = ParameterLocation.Header
            },
            Array.Empty<string>()
        }
    });

});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();

