using System.Threading.Tasks;

namespace Service.API1.BLL.Contracts
    {
    public interface IJwtTokenService
        {
        Task<string> GenerateToken();
        Task<bool> ValidateToken(string token);
        }
    }
