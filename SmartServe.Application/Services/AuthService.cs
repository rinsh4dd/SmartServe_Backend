using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs.AuthDto;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(IAuthRepository authRepository, IJwtTokenService jwt)
    {
        _authRepository = authRepository;
        _jwtTokenService = jwt;
    }

    public async Task<AuthReponseDto> RegisterUserAsync(RegisterUserDto dto)
    {
        dto.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        dto.UserEmail = dto.UserEmail.ToLower().Trim();


        int result = await _authRepository.RegisterUserAsync(dto);

        if (result == -1)
            return new AuthReponseDto(400, "Email already used");

        if (result > 0)
            return new AuthReponseDto(201, "User registered successfully!");

        return new AuthReponseDto(400, "Registration failed");
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
