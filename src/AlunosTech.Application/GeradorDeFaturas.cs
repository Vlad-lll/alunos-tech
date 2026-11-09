using AlunosTech.Domain;

namespace AlunosTech.Application;

/// <summary>
/// Caso de uso: gera a fatura de um contrato para uma competência,
/// escolhendo a regra de cobrança pelo tipo do contrato.
/// </summary>
public class GeradorDeFaturas
{
    private readonly RegraDeCobrancaFactory _factory;

    public GeradorDeFaturas(RegraDeCobrancaFactory factory) => _factory = factory;

    public Fatura Gerar(Contrato contrato, DateOnly competencia)
    {
        var regra = _factory.Criar(contrato.Tipo);
        var valor = regra.CalcularValor(contrato, competencia);
        return new Fatura(contrato.Id, competencia, valor);
    }
}
