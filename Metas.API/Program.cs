#region BUILDER
using Metas.API.EndPoints;
using Metas.shared.Dados.Banco;
using Metas.shared.Dados.Modelos;
using Metas.Shared.Dados.Banco;
using Metas.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
    options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<MetasContext>();


builder.Services
    .AddIdentityApiEndpoints<PessoaComAcesso>()
    .AddEntityFrameworkStores<MetasContext>();



builder.Services.AddAuthorization();

builder.Services.AddTransient<DAL<PessoaComAcesso>>();
builder.Services.AddTransient<DAL<Meta>>();
builder.Services.AddTransient<DAL<Passos>>();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();



builder.Services.AddCors(options => options.AddPolicy(
    "wasm",
    policy => policy
        .WithOrigins(
        [
            builder.Configuration["BackendUrl"] ?? "https://localhost:7089",
            builder.Configuration["FrontendUrl"] ?? "https://localhost:7015",
            "https://localhost:7210", // Outra porta para o backend
            "https://localhost:7002"  // Outra porta para o frontend
        ])
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()));


var app = builder.Build();

#endregion

#region UseConfigs

app.UseCors("wasm");
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();



#endregion


#region ChamadaDeEndPoints

app.AddEndPointsMetas();
app.AddEndPointsPassos();



app.MapGroup("auth").MapIdentityApi<PessoaComAcesso>().WithTags("Autorizacao");

app.MapPost("auth/logout", async ([FromServices] SignInManager<PessoaComAcesso> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();

}).RequireAuthorization().WithTags("Autorizacao");

#endregion


app.UseSwagger();
app.UseSwaggerUI();


app.Run();


