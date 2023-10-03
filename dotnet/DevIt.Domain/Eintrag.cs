using FluentValidation;

namespace com.b_velop.DevIt.Domain;

/// <summary>
/// Stellt einen Eintrag dar.
/// </summary>
public class Eintrag
{
  /// <summary>
  /// Der Primärschlüssel.
  /// </summary>
  public int Id { get; private set; }

  public string Text { get; private set; }
  public double Stunden { get; private set; }
  public DateTimeOffset Datum { get; private set; }
  public bool Abrechenbar { get; private set; }
  public string? ExterneId { get; private set; }

  private Eintrag()
  {
  }

  public static Eintrag Create(CreateEintrag command)
    => command.Validate(new Eintrag
    {
      Id = 0,
      Text = command.Text,
      Abrechenbar = command.Abrechenbar,
      Datum = command.Datum,
      Stunden = command.Stunden,
      ExterneId = command.ExterneId
    });

  public static Eintrag Update(UpdateEintrag command, Eintrag oldEintrag)
  {
    oldEintrag.Text = command.Text;
    oldEintrag.Abrechenbar = command.Abrechenbar;
    oldEintrag.Datum = command.Datum;
    oldEintrag.Stunden = command.Stunden;
    oldEintrag.ExterneId = command.ExterneId;
    return command.Validate(oldEintrag);
  }

  public class CreateEintrag : ICommand<Eintrag>
  {
    public string Text { get; }
    public double Stunden { get; }
    public DateTimeOffset Datum { get; }
    public bool Abrechenbar { get; }
    public string? ExterneId { get; }

    public class EintragValidator : AbstractValidator<Eintrag>
    {
      public EintragValidator()
      {
        RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Stunden).InclusiveBetween(0, 24);
        RuleFor(x => x.Datum).ExclusiveBetween(DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
      }
    }

    public Eintrag Validate(Eintrag eintrag)
    {
      var result = _validator.Validate(eintrag);
      if (!result.IsValid)
        throw new ValidationException(result.Errors);
      return eintrag;
    }

    private IValidator<Eintrag> _validator;

    public CreateEintrag(string text, double stunden, DateTimeOffset datum, bool abrechenbar,
      string? externeId = null)
    {
      _validator = new EintragValidator();
      Text = text;
      Stunden = stunden;
      Datum = datum;
      Abrechenbar = abrechenbar;
      ExterneId = externeId;
    }
  }

  public class UpdateEintrag : ICommand<Eintrag>
  {
    public string Text { get; }
    public double Stunden { get; }
    public DateTimeOffset Datum { get; }
    public bool Abrechenbar { get; }
    public string? ExterneId { get; }

    public class EintragValidator : AbstractValidator<Eintrag>
    {
      public EintragValidator()
      {
        RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Stunden).InclusiveBetween(0, 24);
        RuleFor(x => x.Datum).ExclusiveBetween(DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
      }
    }

    public Eintrag Validate(Eintrag eintrag)
    {
      var result = _validator.Validate(eintrag);
      if (!result.IsValid)
        throw new ValidationException(result.Errors);
      return eintrag;
    }

    private IValidator<Eintrag> _validator;

    public UpdateEintrag(string text, double stunden, DateTimeOffset datum, bool abrechenbar,
      string? externeId = null)
    {
      _validator = new EintragValidator();
      Text = text;
      Stunden = stunden;
      Datum = datum;
      Abrechenbar = abrechenbar;
      ExterneId = externeId;
    }
  }
}
