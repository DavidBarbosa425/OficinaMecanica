using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OficinaMecanica.Application.Cadastros;
using OficinaMecanica.Application.Catalogo;
using OficinaMecanica.Application.OrdensServico;
using OficinaMecanica.Application.Sistema;
using OficinaMecanica.Infrastructure.Identity;
using OficinaMecanica.Infrastructure.Notifications;
using OficinaMecanica.Infrastructure.Persistence;

namespace OficinaMecanica.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSmtpNotifications(this IServiceCollection services, IConfiguration configuration)
    {
        var smtp = SmtpConfiguracao.Ler(configuration);
        var notificacoes = NotificacoesStatusConfiguracao.Ler(configuration);
        services.AddSingleton(smtp);
        services.AddSingleton(notificacoes);
        services.AddScoped<INotificadorStatusOrdemServico, NotificadorStatusSmtp>();
        services.AddScoped<ProcessadorNotificacoesStatus>();
        if (smtp.Enabled && notificacoes.ProcessadorAtivo)
            services.AddHostedService<ServicoHospedadoNotificacoesStatus>();
        return services;
    }

    public static IServiceCollection AddSqlServerPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Oficina");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Defina ConnectionStrings__Oficina para conectar ao SQL Server.");
        }

        services.AddDbContext<OficinaDbContext>(options =>
            options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()));
        services.AddScoped<IConsultaSaudeBanco, ConsultaSaudeBanco>();
        services.AddScoped<ICadastroRepository, CadastroRepository>();
        services.AddScoped<ICatalogoRepository, CatalogoRepository>();
        services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
        services.AddScoped<INotificacaoStatusRepository, NotificacaoStatusRepository>();
        services.AddScoped<IConsultaGestaoOrdemServicoRepository, ConsultaGestaoOrdemServicoRepository>();
        services.AddScoped<IReservaEstoqueRepository, ReservaEstoqueRepository>();
        services.AddScoped<IExecucaoOrdemServicoRepository, ExecucaoOrdemServicoRepository>();
        services.AddScoped<IAditivoRepository, AditivoRepository>();

        services.AddDbContext<IdentidadeDbContext>(options =>
            options.UseSqlServer(connectionString, sql => sql
                .MigrationsHistoryTable("__HistoricoMigrationsIdentidade")
                .EnableRetryOnFailure()));

        return services;
    }
}
