namespace AlunosTech.Domain;

public class Fatura : Entidade
{
    public int ContratoId { get; private set; }
    public DateOnly Competencia { get; private set; }
    public decimal Valor { get; private set; }
    public StatusFatura Status { get; private set; } = StatusFatura.Aberta;

    private Fatura() { } // usado pelo Entity Framework

    public Fatura(int contratoId, DateOnly competencia, decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor da fatura deve ser positivo.");

        ContratoId = contratoId;
        Competencia = competencia;
        Valor = valor;
    }

    public void MarcarComoPaga()
    {
        if (Status == StatusFatura.Cancelada)
            throw new InvalidOperationException("Fatura cancelada não pode ser paga.");
        Status = StatusFatura.Paga;
    }

    public void Cancelar()
    {
        if (Status == StatusFatura.Paga)
            throw new InvalidOperationException("Fatura paga não pode ser cancelada.");
        Status = StatusFatura.Cancelada;
    }
}
