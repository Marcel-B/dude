using MediatR;

namespace DevIt.Projekt.Adapter.Query;

public class GetProjektByIdQuery : IRequest<Domain.Projekt>
{
  public string Id { get; set; }
}
