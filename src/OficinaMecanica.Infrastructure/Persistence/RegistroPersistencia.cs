namespace OficinaMecanica.Infrastructure.Persistence;

public sealed class RegistroPersistencia
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTimeOffset CriadoEm { get; set; }
}
