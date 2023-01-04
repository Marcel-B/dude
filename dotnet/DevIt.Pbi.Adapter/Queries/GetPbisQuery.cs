using MediatR;

namespace DevIt.Pbi.Adapter.Queries;

public class GetPbisQuery : IRequest<IList<Domain.Pbi>>
{

}
