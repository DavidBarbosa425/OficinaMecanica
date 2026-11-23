using OficinaMecanica.Application.Sistema;

namespace OficinaMecanica.Infrastructure.Persistence;

internal sealed class ConsultaSaudeBanco(OficinaDbContext db) : IConsultaSaudeBanco
{
    public Task<bool> PodeConectarAsync(CancellationToken cancelamento) =>
        db.Database.CanConnectAsync(cancelamento);
}
