using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AlunosTech.Tests;

// Teste de integração: sobe a API em memória e chama um endpoint de verdade.
public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ApiIntegrationTests(WebApplicationFactory<Program> factory) => _factory = factory;

    [Fact]
    public async Task Health_responde_200()
    {
        var client = _factory.CreateClient();

        var resposta = await client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
    }
}
