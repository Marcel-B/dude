using MediatR;

namespace DevIt.Projekt.Adapter.Commands;

public class CreateProjektCommand : IRequest<com.b_velop.DevIt.Domain.Projekt>
{
    public CreateProjektCommand(
        string name,
        string? externeId = null)
    {
        Name = name;
        ExterneId = externeId;
    }

    public string Name { get; }
    public string? ExterneId { get; }
}