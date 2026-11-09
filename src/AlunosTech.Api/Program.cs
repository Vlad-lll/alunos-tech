using AlunosTech.Application;
using AlunosTech.Domain;
using AlunosTech.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("AlunosTech") ?? "";
builder.Services.AddInfrastructure(connectionString);
builder.Services.AddScoped<RegraDeCobrancaFactory>();
builder.Services.AddScoped<GeradorDeFaturas>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Usado pelas sondas de saúde (liveness/readiness) do Kubernetes
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

// Lista as faturas em aberto (precisa do banco criado com os scripts de /database)
app.MapGet("/faturas/abertas", async (IFaturaRepository repo, CancellationToken ct) =>
{
    var faturas = await repo.ListarAbertasAsync(ct);
    return Results.Ok(faturas.Select(f => new
    {
        f.Id,
        f.ContratoId,
        Competencia = f.Competencia.ToString("yyyy-MM-dd"),
        f.Valor,
        Status = f.Status.ToString()
    }));
});

app.Run();

// Necessário para os testes de integração com WebApplicationFactory
public partial class Program { }
