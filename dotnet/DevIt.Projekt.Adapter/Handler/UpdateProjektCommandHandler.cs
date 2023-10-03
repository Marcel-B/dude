using com.b_velop.DevIt.Domain;
using DevIt.Projekt.Adapter.Commands;
using DevIt.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Projekt.Adapter.Handler;

public class UpdateProjektCommandHandler : IRequestHandler<UpdateProjektCommand, com.b_velop.DevIt.Domain.Projekt>
{
    private readonly IServiceProvider _serviceProvider;

    public UpdateProjektCommandHandler(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<com.b_velop.DevIt.Domain.Projekt> Handle(
        UpdateProjektCommand request,
        CancellationToken cancellationToken)
    {
        var create = new CreateProjekt(request.Name, request.ExterneId);
        var projekt = com.b_velop.DevIt.Domain.Projekt.Create(create);
        var uow = _serviceProvider.GetRequiredService<IUnitOfWork>();
        await uow.Projekte.UpdateProjektAsync(projekt, cancellationToken);
        await uow.CompleteAsync(cancellationToken);
        return projekt;
    }
}