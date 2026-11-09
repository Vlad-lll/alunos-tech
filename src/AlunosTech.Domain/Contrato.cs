namespace AlunosTech.Domain;

public class Contrato : Entidade
{
    public int ClienteId { get; private set; }
    public TipoContrato Tipo { get; private set; }
    public decimal ValorMensal { get; private set; }
    public decimal ValorPorUnidade { get; private set; }
    public DateOnly Inicio { get; private set; }
    public DateOnly? Fim { get; private set; }

    // Consumo do mês corrente, usado nos contratos de cobrança por uso.
    public decimal UnidadesConsumidas { get; private set; }

    private Contrato() { } // usado pelo Entity Framework

    public Contrato(int clienteId, TipoContrato tipo, decimal valorMensal,
                    decimal valorPorUnidade, DateOnly inicio)
    {
        ClienteId = clienteId;
        Tipo = tipo;
        ValorMensal = valorMensal;
        ValorPorUnidade = valorPorUnidade;
        Inicio = inicio;
    }

    public void RegistrarConsumo(decimal unidades)
    {
        if (unidades < 0)
            throw new ArgumentException("O consumo não pode ser negativo.");
        UnidadesConsumidas += unidades;
    }
}
