using CRMDataManager.Models;
using System.Threading.Tasks;

namespace CRMDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}