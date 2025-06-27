using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;

namespace InventoryManagementSystem.Repositories
{
    public class AppUserRepo : IAppUserRepo
    {
        private readonly AppDbContext db;

        public AppUserRepo(AppDbContext db)
        {
            this.db = db;
        }
        public void Add(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetAll()
        {
           return db.ApplicationUsers.ToList();
        }

        public string GetRoleByUserId (string id)
        {
            string RoleId = db.UserRoles.FirstOrDefault(u => u.UserId == id)?.RoleId;
            string userRole = db.Roles.FirstOrDefault(u => u.Id == RoleId)?.Name;

            return userRole;
        }

        public void GetAllUsersRoles()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetUserById(string id)
        {
           ApplicationUser user= db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            return user;
        }
    }
}
