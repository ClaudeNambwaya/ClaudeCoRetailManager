using CRMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMDesktopUI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}