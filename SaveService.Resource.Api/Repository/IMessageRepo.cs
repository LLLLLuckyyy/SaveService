using System.Threading.Tasks;

namespace SaveService.Resources.Api.Repository
{
    public interface IMessageRepo
    {
        Task<string> GetAsync(int IdOfMessage, string login);
        Task SaveAsync(string text, string login);
        Task DeleteAsync(int IdOfMessage, string login);
        Task EditAsync(string text, int IdOfMessage, string login);
    }
}
