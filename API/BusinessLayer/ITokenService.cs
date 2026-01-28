namespace API.BusinessLayer
{
    public interface ITokenService
    {
        string GenerateToken(string username);
    }
}
