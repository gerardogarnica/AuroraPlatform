using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<int>
{
    public string Email { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Notes { get; init; }
}

public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ICredentialLogRepository _credentialLogRepository;

    #endregion

    #region Constructor

    public CreateUserHandler(
        IMapper mapper,
        IUserRepository userRepository,
        ICredentialLogRepository credentialLogRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _credentialLogRepository = credentialLogRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<int> IRequestHandler<CreateUserCommand, int>.Handle(
        CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Create user entity
        var user = _mapper.Map<User>(request);
        user.IsActive = true;
        user.EncryptPassword(request.Email, DateTime.Today);

        // Add user repository
        user = await _userRepository.AddAsync(user);

        // Add credential log repository
        var credentialLog = new CredentialLog(user, 0);
        await _credentialLogRepository.AddAsync(credentialLog);

        // Returns entity ID
        return user.Id;
    }

    #endregion
}