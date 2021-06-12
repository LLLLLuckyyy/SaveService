﻿using Microsoft.AspNetCore.Mvc;
using SaveService.Models;
using System.Net;
using System.Threading.Tasks;

namespace SaveService.Repository
{
    public interface IMessageRepo
    {
        Task<string> GetAsync(int IdOfMessage, string login);
        Task SaveAsync(string text, string login);
        Task DeleteAsync(int IdOfMessage, string login);
        Task EditAsync(string text, int IdOfMessageToChange, string login);
    }
}
