namespace Service.Interfaces;

public interface ITokensService
{
    public ValueTask<string> Generatetoken(string phone, string password);
}
