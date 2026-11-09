namespace AlunosTech.Domain;

public interface IRegraDeCobranca
{
    decimal CalcularValor(Contrato contrato, DateOnly competencia);
}

public class CobrancaMensalFixa : IRegraDeCobranca
{
    public decimal CalcularValor(Contrato contrato, DateOnly competencia)
        => contrato.ValorMensal;
}

public class CobrancaPorUso : IRegraDeCobranca
{
    public decimal CalcularValor(Contrato contrato, DateOnly competencia)
        => Math.Round(contrato.UnidadesConsumidas * contrato.ValorPorUnidade, 2);
}
