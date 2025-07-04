
using InventoryManagementSystem.Models;
using System.Security.Claims;

namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface IWareHouseRepo:IRepository<WareHouse>
    {
        List<WareHouse> sort(string sortparam,ClaimsPrincipal user);
    }
}
