using AlunosTech.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlunosTech.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Fatura> Faturas => Set<Fatura>();
    public DbSet<Contrato> Contratos => Set<Contrato>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // O mapeamento segue o esquema criado em database/01_schema.sql
        modelBuilder.Entity<Fatura>(e =>
        {
            e.ToTable("Fatura");
            e.HasKey(f => f.Id);
            e.Property(f => f.Id).HasColumnName("FaturaId");
            e.Property(f => f.Valor).HasColumnType("decimal(18,2)");
            e.Property(f => f.Status)
             .HasMaxLength(15)
             .HasConversion(
                 status => status.ToString().ToUpperInvariant(),
                 texto => Enum.Parse<StatusFatura>(texto, true));

            // Controle otimista de concorrência (coluna ROWVERSION)
            e.Property<byte[]>("Versao").IsRowVersion();
        });

        modelBuilder.Entity<Contrato>(e =>
        {
            e.ToTable("Contrato");
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).HasColumnName("ContratoId");
            e.Property(c => c.ValorMensal).HasColumnType("decimal(18,2)");
            e.Property(c => c.ValorPorUnidade).HasColumnType("decimal(18,4)");
            e.Property(c => c.Tipo)
             .HasMaxLength(20)
             .HasConversion(
                 tipo => tipo == TipoContrato.PorUso ? "POR_USO" : "MENSAL_FIXO",
                 texto => texto == "POR_USO" ? TipoContrato.PorUso : TipoContrato.MensalidadeFixa);
            e.Ignore(c => c.UnidadesConsumidas);
        });
    }
}
