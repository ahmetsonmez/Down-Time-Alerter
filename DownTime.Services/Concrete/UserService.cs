using DownTime.CommonModel.Models;
using DownTime.Core.Hash;
using DownTime.Data.Context;
using DownTime.Data.Entities;
using DownTime.Data.Repositories;
using DownTime.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace DownTime.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly BaseRepository<User> _repository;
        private readonly ISaltHash _hash;
        public UserService(DbContextOptions<AppDbContext> options, ISaltHash hash)
        {
            _hash = hash;
            _repository = new Repository<User>(options);
        }

        //TODO : I had to write mapper here and for the other dto models but time was limited.

        /// <summary>
        /// Checks if user is exist or not.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UserDto IsLogin(LoginUserDto model)
        {
            var userModel = _repository.Get(x => x.Email == model.Email && x.IsActive);
            if (userModel.Result.Data == null) return null;
            var isMatch = _hash.MatchHash(model.Email + model.Password, userModel.Result.Data.Hash, userModel.Result.Data.Salt);
            if (isMatch)
            {
                return new UserDto
                {
                    Id = userModel.Result.Data.Id,
                    IsActive = userModel.Result.Data.IsActive,
                    Hash = userModel.Result.Data.Hash,
                    Salt = userModel.Result.Data.Salt,
                    Email = userModel.Result.Data.Email,
                    UserName = userModel.Result.Data.UserName,
                    FirstName = userModel.Result.Data.FirstName,
                    LastName = userModel.Result.Data.LastName
                };
            }
            return null;
        }
    }
}
