using com.b_velop.Dude.Shared;

namespace com.b_velop.Dude.Bff.Services;

public class AbrechnungService : IAbrechnungService
{
    private readonly Shared.AbrechnungService.AbrechnungServiceClient _client;

    public AbrechnungService(
        Shared.AbrechnungService.AbrechnungServiceClient client)
    {
        _client = client;
    }

    public async Task<double?> GetAbrechnungByMonat(
        int monat,
        int jahr,
        string text,
        CancellationToken cancellationToken = default)
    {
        var request = new GetByMonatRequest
        {
            Monat = monat,
            Jahr = jahr,
            Text = text
        };
        var result = await _client.GetByMonatAsync(request, cancellationToken: cancellationToken);
        return result?.Stunden;
    }

    public async Task<double?> GetAbrechnungByJahr(
        int jahr,
        string text,
        CancellationToken cancellationToken = default)
    {
        var request = new GetByJahrRequest
        {
            Jahr = jahr,
            Text = text
        };
        var result = await _client.GetByJahrAsync(request, cancellationToken: cancellationToken);
        return result?.Stunden;
    }

    public async Task<double?> GetAbrechnungByKalenderwoche(
        int kalenderwoche,
        int jahr,
        string text,
        CancellationToken cancellationToken = default)
    {
        var request = new GetByKalenderwocheRequest
        {
            Kalenderwoche = kalenderwoche,
            Jahr = jahr,
            Text = text
        };
        var result = await _client.GetByKalenderwocheAsync(request, cancellationToken: cancellationToken);
        return result?.Stunden;
    }
}