using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;

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

        public string GetRoleOfUser (string userId)
        {
            string RoleId = db.UserRoles.FirstOrDefault(u => u.UserId == userId)?.RoleId;
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

        public void Update(ApplicationUser user)
        {
            db.ApplicationUsers.Update(user);
        }

        public ApplicationUser GetUserById(string id)
        {
           ApplicationUser user= db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            return user;
        }

        public string GetRoleName(string roleId)
        {
            string roleName=db.Roles.SingleOrDefault(r => r.Id == roleId)?.Name;
            return roleName;
        }

        public void DeleteUser(ApplicationUser user)
        {
            db.ApplicationUsers.Remove(user);
        }

        public List<ApplicationUser> GetAllManagers()
        {
          var managers=  (from user in db.ApplicationUsers
             join userRole in db.UserRoles
             on user.Id equals userRole.UserId
             join role in db.Roles
             on userRole.RoleId equals role.Id
             where role.Name == "Manager"
             select new ApplicationUser
             {
                 Id = user.Id,
                 UserName = user.UserName
             }).ToList();

            return managers;

        }

        public List<ApplicationUser> sort(string sortOrder)
        {
            var users = GetAll().AsQueryable();
            foreach (var user in users)
            {
                string userRole = GetRoleOfUser(user.Id);
                user.Role = userRole;
            }

            users = sortOrder switch
            {
               
                "name_desc" => users.OrderByDescending(u => u.UserName),
                "email" => users.OrderBy(u => u.Email),
                "email_desc" => users.OrderByDescending(u => u.Email),
                "phone" => users.OrderBy(u => u.PhoneNumber),
                "phone_desc" => users.OrderByDescending(u => u.PhoneNumber),
                "address" => users.OrderBy(u => u.Address),
                "address_desc" => users.OrderByDescending(u => u.Address),
                "role" => users.OrderBy(u => u.Role),
                "role_desc" => users.OrderByDescending(u => u.Role),
                _ => users.OrderBy(u => u.UserName),
            };
            return users.ToList();
        }
    }
}
