using MediatR;

namespace DevIt.Projekt.Adapter.Query;

public class GetProjekteQuery : IRequest<IEnumerable<Domain.Projekt>>
{
}
