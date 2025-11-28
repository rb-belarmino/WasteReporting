using WasteReporting.API.ViewModels;

namespace WasteReporting.API.Services;

public interface IAuthService
{
    Task<AuthResponseViewModel> RegisterAsync(RegisterViewModel dto);
    Task<AuthResponseViewModel> LoginAsync(LoginViewModel dto);
}
