namespace Diving.Application.DeleteClient;

public record DeleteClientCommand(long ClientId) : ICommand;

internal class DeleteClientCommandHandler : UnitOfWorkCommandHandler<DeleteClientCommand>
{
    private readonly IClientRepository _clientRepository;

    public DeleteClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _clientRepository = clientRepository;
    }

    public override async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        await _clientRepository.Delete(request.ClientId, cancellationToken);
        await base.Handle(request, cancellationToken);}
}
