using FluentValidation;

namespace DevIt.Domain;

public class Pbi
{
  public int Id { get; private set; }
  public string Name { get; private set; }
  public string ProjektId { get; private set; }

  public virtual Projekt Projekt { get; }

  private Pbi()
  {
  }

  public static Pbi Create(CreatePbi command)
    => command.Validate(new Pbi
    {
      Id = command.Id,
      Name = command.Name,
      ProjektId = command.ProjektId
    });

  public class CreatePbi : ICommand<Pbi>
  {
    public class PbiValidator : AbstractValidator<Pbi>
    {
      public PbiValidator()
      {
        RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ProjektId).NotEmpty();
      }
    }

    private IValidator<Pbi> _validator;

    public CreatePbi(string name, string projektId, int id = 0)
    {
      _validator = new PbiValidator();
      Name = name;
      ProjektId = projektId;
      Id = id;
    }

    public int Id { get; }
    public string Name { get; }
    public string ProjektId { get; }

    public Pbi Validate(Pbi pbi)
    {
      var result = _validator.Validate(pbi);
      if (!result.IsValid)
        throw new ValidationException(result.Errors);
      return pbi;
    }
  }
}
