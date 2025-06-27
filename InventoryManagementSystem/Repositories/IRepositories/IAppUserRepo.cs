using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface IAppUserRepo:IRepository<ApplicationUser>
    {
        ApplicationUser GetUserById(string id);
        string GetRoleByUserId(string id);
        void GetAllUsersRoles();
    }
}
