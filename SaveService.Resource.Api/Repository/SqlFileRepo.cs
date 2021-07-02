using Microsoft.AspNetCore.Http;
using SaveService.Resources.Api.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SaveService.Common.Validation;
using SaveService.Common.FileConversion;

namespace SaveService.Resources.Api.Repository
{
    public class SqlFileRepo : IFileRepo
    {
        private readonly UserContext _context;

        public SqlFileRepo(UserContext context)
        {
            _context = context;
        }
        
        public async Task<string> GetAsync(int IdOfFile, string login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
            var file = await _context.Files
                .FirstOrDefaultAsync(f => f.Id == IdOfFile && f.UserId == user.Id);
            return Convert.ToBase64String(file.File);
        }

        public async Task DeleteAsync(int IdOfFile, string login)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
                var file = await _context.Files.FirstOrDefaultAsync(f => f.Id == IdOfFile && f.UserId == user.Id);
                if (DateValidation.IsEnoughTimeHasPassedToEditOrDelete(file.CreatedAt))
                {
                    _context.Files.Remove(file);
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


        public async Task SaveAsync(IFormFile fileToSave, string login)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
                var file = new FileModel();

                file.UserId = user.Id;
                file.File = FileHandler.ConvertFileToBiteArray(fileToSave);
                file.CreatedAt = DateTime.Now;

                await _context.Files.AddAsync(file);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task EditAsync(IFormFile fileToSave, int IdOfFileToChange, string login)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
                var fileToChange = await _context.Files.FirstOrDefaultAsync(f => f.Id == IdOfFileToChange && f.UserId == user.Id);
                if (DateValidation.IsEnoughTimeHasPassedToEditOrDelete(fileToChange.CreatedAt))
                {
                    fileToChange.File = FileHandler.ConvertFileToBiteArray(fileToSave);

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
