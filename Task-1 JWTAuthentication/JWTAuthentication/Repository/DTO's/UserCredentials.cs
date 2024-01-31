namespace JWTAuthentication.Repository.DTOs
{
    public record struct UserCrendentialsDTO
    (
        string? Username,
        string? Password
    );
}