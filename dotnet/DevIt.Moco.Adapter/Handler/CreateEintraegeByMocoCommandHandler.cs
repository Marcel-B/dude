using System.Text.Json;
using DevIt.Domain;
using DevIt.Moco.Adapter.Commands;
using DevIt.Moco.Adapter.Queries;
using DevIt.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevIt.Moco.Adapter.Handler;

public class CreateEintraegeByMocoCommandHandler : IRequestHandler<CreateEintraegeByMocoCommand, Unit>
{
  private readonly IMediator _mediator;
  private readonly IUnitOfWork _uow;

  public CreateEintraegeByMocoCommandHandler(IMediator mediator, IUnitOfWork uow)
  {
    _mediator = mediator;
    _uow = uow;
  }

  public async Task<Unit> Handle(CreateEintraegeByMocoCommand request, CancellationToken cancellationToken)
  {
    var query = new GetActivitiesQuery
    {
      ProjectIds = request.ProjektIds,
      From = request.Monat?.ToFirstDayOfMonth(),
      To = request.Monat?.ToLastDayOfMonth(),
    };

    var activities = await _mediator.Send(query, cancellationToken);
    var frischeEintraege = activities
      .Select(x => x.ToEintrag())
      .DistinctBy(x => x.ExterneId)
      .GroupBy(x => x.Datum)
      .ToList();

    var datumFrischeEintraege = frischeEintraege
      .Select(x => x.Key)
      .ToList();

    var alteEintraege = await _uow.Eintraege
      .Query()
      .Where(x => x.Text == "Solverest" && datumFrischeEintraege.Contains(x.Datum))
      .ToListAsync(cancellationToken);

    var neueEintraege = frischeEintraege.Where(x =>
        !alteEintraege.Select(y => y.Datum).Contains(x.Key))
      .Select(g =>
      {
        var cmd = new Eintrag.CreateEintrag(
          "Solverest",
          g.Sum(x => x.Stunden),
          g.Key,
          true,
          JsonSerializer.Serialize(g.Select(x => x.ExterneId).ToList(),
            new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = false}));
        return Eintrag.Create(cmd);
      }).ToList();

    foreach (var eintrag in alteEintraege)
    {
      var neue = frischeEintraege.First(x => x.Key == eintrag.Datum);
      var cmd = new Eintrag.UpdateEintrag(
        "Solverest",
        neue.Sum(x => x.Stunden),
        neue.Key,
        true,
        JsonSerializer.Serialize(neue.Select(x => x.ExterneId).ToList(),
          new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = false}));
      Eintrag.Update(cmd, eintrag);
    }

    await _uow.Eintraege.CreateEintraegeAsync(neueEintraege, cancellationToken);
    await _uow.Eintraege.UpdateEintraegeAsync(alteEintraege, cancellationToken);
    await _uow.CompleteAsync(cancellationToken);
    return Unit.Value;
  }
}
