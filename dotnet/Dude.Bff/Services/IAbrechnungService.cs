namespace com.b_velop.Dude.Bff.Services;

public interface IAbrechnungService
{
    Task<double?> GetAbrechnungByMonat(
        int monat,
        int jahr,
        string text,
        CancellationToken cancellationToken = default);

    Task<double?> GetAbrechnungByJahr(
        int jahr,
        string text,
        CancellationToken cancellationToken = default);

    Task<double?> GetAbrechnungByKalenderwoche(
        int kalenderwoche,
        int jahr,
        string text,
        CancellationToken cancellationToken = default);

    public Task<IEnumerable<string>> GetProjekte(
        CancellationToken cancellationToken = default);
}