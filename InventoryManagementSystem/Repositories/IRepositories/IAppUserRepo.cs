using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface IAppUserRepo:IRepository<ApplicationUser>
    {
        ApplicationUser GetUserById(string id);
        string GetRoleOfUser(string userId);
        void DeleteUser(ApplicationUser user);
        void GetAllUsersRoles();
        string GetRoleName(string roleId);
        List<ApplicationUser> GetAllManagers();
    }
}
