using FluentValidation;

namespace DevIt.Domain;

public class Eintrag
{
  public int Id { get; private set; }
  public string Text { get; private set; }
  public double Stunden { get; private set; }
  public DateTimeOffset Datum { get; private set; }
  public bool Abrechenbar { get; private set; }

  private Eintrag()
  {
  }

  public static Eintrag Create(CreateEintrag command)
    => command.Validate(new Eintrag
    {
      Id = command.Id,
      Text = command.Text,
      Abrechenbar = command.Abrechenbar,
      Datum = command.Datum,
      Stunden = command.Stunden
    });

  public class CreateEintrag : ICommand<Eintrag>
  {
    public string Text { get; }
    public double Stunden { get; }
    public DateTimeOffset Datum { get; }
    public bool Abrechenbar { get; }
    public int Id { get; }

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

    public CreateEintrag(string text, double stunden, DateTimeOffset datum, bool abrechenbar, int id = 0)
    {
      _validator = new EintragValidator();
      Id = id;
      Text = text;
      Stunden = stunden;
      Datum = datum;
      Abrechenbar = abrechenbar;
    }
  }
}
