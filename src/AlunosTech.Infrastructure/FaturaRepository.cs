using AlunosTech.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlunosTech.Infrastructure;

public class FaturaRepository : IFaturaRepository
{
    private readonly AppDbContext _db;

    public FaturaRepository(AppDbContext db) => _db = db;

    public Task<Fatura?> ObterPorIdAsync(int id, CancellationToken ct)
        => _db.Faturas.FirstOrDefaultAsync(f => f.Id == id, ct);

    public async Task<IReadOnlyList<Fatura>> ListarAbertasAsync(CancellationToken ct)
        => await _db.Faturas
                    .AsNoTracking()
                    .Where(f => f.Status == StatusFatura.Aberta)
                    .ToListAsync(ct);

    public async Task AdicionarAsync(Fatura fatura, CancellationToken ct)
    {
        await _db.Faturas.AddAsync(fatura, ct);
        await _db.SaveChangesAsync(ct);
    }
}
