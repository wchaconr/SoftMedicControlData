using Microsoft.EntityFrameworkCore;
using SoftMedicControlData.Components;
using SoftMedicControlData.Components.Pages;
using SoftMedicControlData.Data;
using SoftMedicControlData.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionLocal"))
);

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoServices>();
builder.Services.AddScoped<CitaServices>();
builder.Services.AddScoped<HistoriaClinicaServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseDeveloperExceptionPage(); //  muestra los detalles de las excepciones
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SoftMedicControlData.Client._Imports).Assembly);

app.Run();
