using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SaveService.Resources.Api.Models;
using SaveService.Common.Validation;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SaveService.Resources.Api.Repository
{
    public class SqlMessageRepo : IMessageRepo
    {
        private readonly UserContext _context;

        public SqlMessageRepo(UserContext context)
        {
            _context = context;
        }


        public async Task<string> GetAsync(int IdOfMessage, string login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
            var messageOfUser = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == IdOfMessage && m.UserId == user.Id);
            return messageOfUser.Text;
        }

        public async Task DeleteAsync(int IdOfMessage, string login)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
                var messageOfUser = await _context.Messages
                    .FirstOrDefaultAsync(m => m.Id == IdOfMessage && m.UserId == user.Id);

                if (DateValidation.IsAllowedToChange(messageOfUser.CreatedAt))
                {
                    _context.Messages.Remove(messageOfUser);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    throw new MemberAccessException();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task SaveAsync(string text, string login)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
                var message = new MessageModel() { Text = text, UserId = user.Id, CreatedAt = DateTime.Now };
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
                
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task EditAsync(string text, int IdOfMessageToChange, string login)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
                var message = await _context.Messages
                    .FirstOrDefaultAsync(m => m.Id == IdOfMessageToChange && m.UserId == user.Id);

                if (DateValidation.IsAllowedToChange(message.CreatedAt))
                {
                    message.Text = text;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    throw new MemberAccessException();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
