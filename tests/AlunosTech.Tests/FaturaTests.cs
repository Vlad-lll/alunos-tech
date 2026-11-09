using AlunosTech.Domain;
using Xunit;

namespace AlunosTech.Tests;

public class FaturaTests
{
    private static readonly DateOnly Competencia = new(2026, 11, 1);

    [Fact]
    public void Nova_fatura_comeca_aberta()
    {
        var fatura = new Fatura(1, Competencia, 100m);

        Assert.Equal(StatusFatura.Aberta, fatura.Status);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Fatura_com_valor_invalido_lanca_excecao(int valor)
    {
        Assert.Throws<ArgumentException>(() => new Fatura(1, Competencia, valor));
    }

    [Fact]
    public void Fatura_paga_nao_pode_ser_cancelada()
    {
        var fatura = new Fatura(1, Competencia, 100m);
        fatura.MarcarComoPaga();

        Assert.Throws<InvalidOperationException>(() => fatura.Cancelar());
    }

    [Fact]
    public void Fatura_aberta_pode_ser_cancelada()
    {
        var fatura = new Fatura(1, Competencia, 100m);

        fatura.Cancelar();

        Assert.Equal(StatusFatura.Cancelada, fatura.Status);
    }
}
