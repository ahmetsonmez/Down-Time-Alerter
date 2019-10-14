using DownTime.CommonModel.Models;

namespace DownTime.Services.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// Checks if user is exist or not.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        UserDto IsLogin(LoginUserDto model);
    }
}
