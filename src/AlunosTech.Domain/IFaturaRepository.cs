namespace AlunosTech.Domain;

public interface IFaturaRepository
{
    Task<Fatura?> ObterPorIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Fatura>> ListarAbertasAsync(CancellationToken ct);
    Task AdicionarAsync(Fatura fatura, CancellationToken ct);
}
