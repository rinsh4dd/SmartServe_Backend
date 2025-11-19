using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs.AuthDto;
using SmartServe.Domain.Enums;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ICustomerRespository _customerRespository;


    public AuthService(IAuthRepository authRepository, IJwtTokenService jwt, ICustomerRespository customerRespository)
    {
        _authRepository = authRepository;
        _jwtTokenService = jwt;
        _customerRespository = customerRespository;
    }
    public async Task<AuthReponseDto> RegisterUserAsync(RegisterUserDto dto)
    {
        dto.UserEmail = dto.UserEmail.ToLower().Trim();
        dto.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        using (var connection = _authRepository.GetConnection())
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // 1️⃣ INSERT USER
                    int userId = await _authRepository.RegisterUserAsync(dto, transaction);
                    if (userId == -1)
                        return new AuthReponseDto(400, "Email already used");

                    // 2️⃣ INSERT CUSTOMER
                    if (dto.Role == Roles.Customer)
                    {
                        await _customerRespository.AddCustomerForUserAsync(
                            userId,
                            dto.UserEmail,
                            dto.UserName,
                            transaction
                        );
                    }

                    // ALL GOOD → Commit
                    transaction.Commit();
                    return new AuthReponseDto(201, "User registered successfully!");
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }


    public async Task<AuthReponseDto> LoginUserAsync(LoginRequestDto dto)
    {
        string email = dto.UserEmail.Trim().ToLower();

        var userRow = await _authRepository.GetUserByEmailAsync(email);

        if (userRow == null)
            return new AuthReponseDto(401, "Invalid email or password");

        if (userRow.IsDeleted)
            return new AuthReponseDto(403, "Account has been deleted");

        bool isMatch = BCrypt.Net.BCrypt.Verify(dto.Password.Trim(), userRow.PasswordHash);

        if (!isMatch)
            return new AuthReponseDto(401, "Invalid email or password");

        if (!userRow.IsActive)
            return new AuthReponseDto(403, "Account is inactive");

        string token = _jwtTokenService.GenerateJwtToken(userRow);

        return new AuthReponseDto(200, "Login successful", token);
    }
}
