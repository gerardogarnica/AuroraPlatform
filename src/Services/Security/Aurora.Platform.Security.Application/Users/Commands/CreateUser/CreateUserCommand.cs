using Aurora.Framework.Cryptography;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<int>
{
    public string LoginName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
}

public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    #endregion

    #region Constructor

    public CreateUserHandler(
        IMapper mapper,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<int> IRequestHandler<CreateUserCommand, int>.Handle(
        CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Create user entity
        var entry = _mapper.Map<User>(request);
        entry.Credential = CreateCredential(request.LoginName);
        entry.Token = CreateToken();

        // Add user repository
        entry = await _userRepository.AddAsync(entry);

        // Returns entity ID
        return entry.Id;
    }

    #endregion

    #region Private methods

    private UserCredential CreateCredential(string loginName)
    {
        var passwordTuple = SymmetricEncryptionProvider.Protect(loginName, "Aurora.Platform.Security.UserData");

        return new UserCredential()
        {
            Password = passwordTuple.Item1,
            PasswordControl = passwordTuple.Item2,
            MustChange = true,
            ExpirationDate = DateTime.Today,
            CredentialLogs = CreateCredentialLogs(passwordTuple.Item1, passwordTuple.Item2)
        };
    }

    private List<UserCredentialLog> CreateCredentialLogs(string password, string control)
    {
        return new List<UserCredentialLog>
            {
                new UserCredentialLog()
                {
                    ChangeVersion = 1,
                    Password = password,
                    PasswordControl = control,
                    MustChange = true,
                    ExpirationDate = DateTime.Today,
                    CreatedDate = DateTime.Today
                }
            };
    }

    private UserToken CreateToken()
    {
        return new UserToken()
        {
            AccessToken = null,
            RefreshToken = null,
            IssuedDate = DateTime.UtcNow,
        };
    }

    #endregion
}