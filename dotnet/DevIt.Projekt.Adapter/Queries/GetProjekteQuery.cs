using MediatR;

namespace DevIt.Projekt.Adapter.Queries;

public class GetProjekteQuery : IRequest<IEnumerable<com.b_velop.DevIt.Domain.Projekt>>
{
}
