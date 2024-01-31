namespace JWTAuthentication.Repository.DTO
{
    public record struct UserCredentialsDTO
    (
        string Username,
        string Password
    );
}