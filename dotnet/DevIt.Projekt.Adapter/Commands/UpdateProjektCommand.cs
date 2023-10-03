using MediatR;

namespace DevIt.Projekt.Adapter.Commands;

public class UpdateProjektCommand : IRequest<com.b_velop.DevIt.Domain.Projekt>
{
    public UpdateProjektCommand(
        int id,
        string name,
        string? externeId = null)
    {
        Id = id;
        Name = name;
        ExterneId = externeId;
    }

    public int Id { get; }
    public string Name { get; }
    public string? ExterneId { get; }
}