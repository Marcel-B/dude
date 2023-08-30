﻿using FluentValidation;

namespace com.b_velop.DevIt.Domain;

public class CreateProjekt : ICommand<Projekt>
{
  public class ProjektValidator : AbstractValidator<Projekt>
  {
    public ProjektValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
      RuleFor(x => x.Id).NotEmpty();
    }
  }

  private IValidator<Projekt> _validator;

  public CreateProjekt(string id, string name, string? externeId = null)
  {
    _validator = new ProjektValidator();
    Id = id;
    Name = name;
    ExterneId = externeId;
  }

  public string Id { get; }
  public string Name { get; }
  public string? ExterneId { get; }

  public Projekt Validate(Projekt projekt)
  {
    var result = _validator.Validate(projekt);
    if (!result.IsValid)
      throw new ValidationException(result.Errors);
    return projekt;
  }
}

public class Projekt
{
  public string Id { get; private set; }
  public string Name { get; private set; }
  public string? ExterneId { get; private set; }
  public virtual ICollection<Pbi> Pbis { get; private set; }

  private Projekt()
  {
  }

  public static Projekt Create(CreateProjekt command)
    => command.Validate(new Projekt
    {
      Id = command.Id,
      Name = command.Name,
      ExterneId = command.ExterneId
    });
}
