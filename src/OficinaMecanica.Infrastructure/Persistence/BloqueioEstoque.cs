using Microsoft.EntityFrameworkCore;
using OficinaMecanica.Domain.Estoque;

namespace OficinaMecanica.Infrastructure.Persistence;

internal static class BloqueioEstoque
{
    public static async Task<Dictionary<Guid, ItemEstoque>> BloquearItensAsync(
        OficinaDbContext db, IReadOnlyCollection<Guid> ids, CancellationToken cancelamento)
    {
        var itens = new Dictionary<Guid, ItemEstoque>();
        foreach (var id in ids)
        {
            var encontrados = await db.ItensEstoque.FromSqlInterpolated(
                $"SELECT * FROM [ItensEstoque] WITH (UPDLOCK, ROWLOCK) WHERE [Id] = {id}")
                .ToListAsync(cancelamento);
            if (encontrados.SingleOrDefault() is { } item) itens.Add(id, item);
        }
        return itens;
    }
}
