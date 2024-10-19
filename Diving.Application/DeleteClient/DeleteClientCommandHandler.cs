namespace Diving.Application.DeleteClient;

public record DeleteClientCommand(long ClientId) : ICommand;

internal class DeleteClientCommandHandler : ICommandHandler<DeleteClientCommand>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        await _clientRepository.Delete(request.ClientId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
