using GrowBotsAlgoplus.Models;

namespace GrowBotsAlgoplus.Connector
{
    public interface IServiceConnector
    {
        Task<bool> sendEmail(Email email);
    }
}
