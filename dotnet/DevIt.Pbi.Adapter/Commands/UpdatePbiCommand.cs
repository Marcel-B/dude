using MediatR;

namespace DevIt.Pbi.Adapter.Commands;

public class UpdatePbiCommand : IRequest<Domain.Pbi>
{
  public int Id { get; }
  public string Name { get; }
  public string ProjektId { get; }
  
  public UpdatePbiCommand(int id, string name, string projektId)
  {
    Id = id;
    Name = name;
    ProjektId = projektId;
  }
}
