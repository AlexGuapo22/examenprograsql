using ExamenReparaciones.CapaLogica;

var builder = WebApplication.CreateBuilder(args);

// La capa de presentacion vive en la carpeta /Presentacion (igual que el proyecto de referencia)
builder.Services.AddRazorPages(o => o.RootDirectory = "/Presentacion");

// Cadena de conexion leida de appsettings.json
DBconn.Cadena = builder.Configuration.GetConnectionString("conexion")!;

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();
app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
