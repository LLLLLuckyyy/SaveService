using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SaveService.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SaveService.Repository
{
    public class SqlUserRepo : IUserRepo
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public SqlUserRepo(UserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task LoginAsync(LoginUser loginModel, Func<string, Task> del)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                UserModel user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Login == loginModel.Login && u.Password == loginModel.Password);
                await del(user.Login);

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task RegisterAsync(RegisterUser registerModel, Func<string, Task> del)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                UserModel userWithSameLogin = await _context.Users.FirstOrDefaultAsync(u => u.Login == registerModel.Login);
                if (userWithSameLogin == null)
                {
                    UserModel newUser = _mapper.Map<UserModel>(registerModel);
                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();
                    await del(newUser.Login);

                    await transaction.CommitAsync();
                }
                else
                {
                    throw new ArgumentException();
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
