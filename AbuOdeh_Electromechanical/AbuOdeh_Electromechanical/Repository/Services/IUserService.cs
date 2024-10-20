using AbuOdeh_Electromechanical.Areas.Admin.Models;

namespace AbuOdeh_Electromechanical.Repository.Services
{
    public interface IUserService
    {
        Task Register(UserRegister viewModel);

        Task<ValidateUserResult> Login(UserLogin viewModel);

        Task Logout();
    }
}
