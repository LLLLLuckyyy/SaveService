using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace SaveService.Resources.Api.Repository
{
    public interface IFileRepo
    {
        Task<string> GetAsync(int IdOfFile, string login);
        Task SaveAsync(IFormFile file, string login);
        Task DeleteAsync(int IdOfFile, string login);
        Task EditAsync(IFormFile file, int IdOfFileToChange, string login);
    }
}
