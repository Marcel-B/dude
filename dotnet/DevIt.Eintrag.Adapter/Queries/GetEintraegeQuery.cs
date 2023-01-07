using MediatR;

namespace DevIt.Eintrag.Adapter.Queries;

public class GetEintraegeQuery : IRequest<IList<Domain.Eintrag>>
{
}
