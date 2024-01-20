using APIFinanceiro.Initializers;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

new BusinessInitializer().Initialize(builder.Services);
new DataInitializer().Initialize(builder.Services);
new SessionInitializer().Initialize(builder.Services);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Financeiro",
        Description = "",
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                       builder =>
                       {
                           builder.AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .AllowAnyOrigin();
                       });
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
}); 

var app = builder.Build();

app.Use((context, next) =>
{
    context.Request.Scheme = "https";
    return next(context);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.PreSerializeFilters.Add((swagger, req) =>
        {
            swagger.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"https://{req.Host}" } };
        });
    });

    app.UseDeveloperExceptionPage();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

//Recomendado para processar corretamente os cabeçalhos encaminhados
app.UseForwardedHeaders();

app.UseRouting();

//Configurar autorização na aplicação
app.UseAuthorization();

app.MapControllers();

app.Run();
