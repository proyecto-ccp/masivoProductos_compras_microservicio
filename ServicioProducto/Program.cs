using Productos.Dominio.Puertos.Repositorios;
using Productos.Infraestructura.Adaptadores.Repositorios;
using Productos.Infraestructura.Adaptadores.RepositorioGenerico;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Productos.Dominio.Servicios.Productos;
using Productos.Dominio.Servicios.Stock;
using Productos.Dominio.Puertos.Integraciones;
using Productos.Infraestructura.Adaptadores.Integraciones;
using Productos.Dominio.Servicios.Ubicaciones;
using ServicioProducto.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "V.2.3.0",
        Title = "Servicio Productos",
        Description = "Administración de productos"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
            Array.Empty<string>()
            }
        });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.Load("Productos.Aplicacion")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Capa Infraestructura
builder.Services.AddDbContext<ProductosDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ProductosDbContext")), ServiceLifetime.Transient);
builder.Services.AddTransient(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));
builder.Services.AddTransient<Productos.Dominio.Puertos.Repositorios.IProductoRepositorio, ProductoRepositorio>();
builder.Services.AddTransient<IAtributoRepositorio, AtributoRepositorio>();
builder.Services.AddTransient<IParametroRepositorio, ParametroRepositorio>();
builder.Services.AddHttpClient<IServicioInventariosApi, ServicioInventariosApi>();
builder.Services.AddTransient<IUbicacionRespositorio, UbicacionRespositorio>();
builder.Services.AddHttpClient<IServicioUsuariosApi, ServicioUsuariosApi>();
//Capa Dominio - Servicios
builder.Services.AddTransient<RegistrarProducto>();
builder.Services.AddTransient<Productos.Dominio.Servicios.Productos.Consultar>();
builder.Services.AddTransient<IngresarInventario>();
builder.Services.AddTransient<ConsultarUbicacion>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<AutorizadorMiddleware>();
app.MapControllers();

await app.RunAsync();
