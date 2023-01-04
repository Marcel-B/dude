using MediatR;

namespace DevIt.Pbi.Adapter.Commands;

public class DeletePbiCommand : IRequest
{
  public int Id { get;  }
  public DeletePbiCommand(int id)
  {
    Id = id;
  }
  
}
