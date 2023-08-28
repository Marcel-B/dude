namespace com.b_velop.Dude.Bff.Services;

public record Eintrag(
    int Id,
    string Text,
    DateTimeOffset Datum,
    double Stunden,
    bool Abrechenbar,
    string ExterneId);