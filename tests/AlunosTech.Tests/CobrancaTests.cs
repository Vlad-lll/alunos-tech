using AlunosTech.Application;
using AlunosTech.Domain;
using Xunit;

namespace AlunosTech.Tests;

public class CobrancaTests
{
    private static readonly DateOnly Inicio = new(2026, 1, 1);
    private static readonly DateOnly Competencia = new(2026, 11, 1);

    [Fact]
    public void Mensalidade_fixa_devolve_o_valor_mensal_do_contrato()
    {
        var contrato = new Contrato(1, TipoContrato.MensalidadeFixa, 1500m, 0m, Inicio);

        var valor = new CobrancaMensalFixa().CalcularValor(contrato, Competencia);

        Assert.Equal(1500m, valor);
    }

    [Fact]
    public void Cobranca_por_uso_multiplica_unidades_pelo_valor_da_unidade()
    {
        var contrato = new Contrato(1, TipoContrato.PorUso, 0m, 2.50m, Inicio);
        contrato.RegistrarConsumo(40);

        var valor = new CobrancaPorUso().CalcularValor(contrato, Competencia);

        Assert.Equal(100m, valor);
    }

    [Theory]
    [InlineData(TipoContrato.MensalidadeFixa, typeof(CobrancaMensalFixa))]
    [InlineData(TipoContrato.PorUso, typeof(CobrancaPorUso))]
    public void Factory_escolhe_a_regra_certa_para_cada_tipo(TipoContrato tipo, Type esperado)
    {
        var regra = new RegraDeCobrancaFactory().Criar(tipo);

        Assert.IsType(esperado, regra);
    }

    [Fact]
    public void Gerador_cria_fatura_com_o_valor_da_regra()
    {
        var contrato = new Contrato(1, TipoContrato.MensalidadeFixa, 800m, 0m, Inicio);
        var gerador = new GeradorDeFaturas(new RegraDeCobrancaFactory());

        var fatura = gerador.Gerar(contrato, Competencia);

        Assert.Equal(800m, fatura.Valor);
        Assert.Equal(Competencia, fatura.Competencia);
    }
}
