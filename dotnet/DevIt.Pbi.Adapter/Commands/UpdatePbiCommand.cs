using MediatR;

namespace DevIt.Pbi.Adapter.Commands;

public class UpdatePbiCommand : IRequest<com.b_velop.DevIt.Domain.Pbi>
{
    public int Id { get; }
    public string Name { get; }
    public int ProjektId { get; }

    public UpdatePbiCommand(
        int id,
        string name,
        int projektId)
    {
        Id = id;
        Name = name;
        ProjektId = projektId;
    }
}