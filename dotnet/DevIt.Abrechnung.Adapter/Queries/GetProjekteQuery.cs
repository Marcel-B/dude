using MediatR;

namespace DevIt.Abrechnung.Adapter.Queries;

public class GetProjekteQuery : IRequest<IEnumerable<string>>
{
}
