namespace Service.Interfaces;

public interface ITokensService
{
    public Task<string> Generatetoken(string phone, string password);
}
