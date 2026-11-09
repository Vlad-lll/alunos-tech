using AlunosTech.Domain;

namespace AlunosTech.Application;

public class RegraDeCobrancaFactory
{
    public IRegraDeCobranca Criar(TipoContrato tipo) => tipo switch
    {
        TipoContrato.MensalidadeFixa => new CobrancaMensalFixa(),
        TipoContrato.PorUso          => new CobrancaPorUso(),
        _ => throw new NotSupportedException($"Tipo de contrato sem regra: {tipo}")
    };
}
