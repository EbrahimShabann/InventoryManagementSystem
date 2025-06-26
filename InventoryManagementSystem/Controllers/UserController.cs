using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using InventoryManagementSystem.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
	//[Area("Admin")]
	//[Authorize(Roles = StaticDetails.Admin_Role)]
	public class UserController : Controller
	{
        private readonly AppDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork uof;

        public UserController(AppDbContext db , UserManager<ApplicationUser> userManager,IUnitOfWork uof)
		{
            this.db = db;
            _userManager = userManager;
            this.uof = uof;
        }
        public IActionResult Index(int page=1, int size=10)
		{
           var UsersList =db.ApplicationUsers.ToList();
            foreach (var user in UsersList)
            {
				string userRole = uof.AppUserRepo.GetRoleByUserId(user.Id);
				user.Role = userRole;
            }
            var paged = UsersList.ToPagedResult(page, size);
          
            return View(paged);
        }
		public IActionResult RoleManagment(string id)
		{
			UserVM userVM;

			var Roles = db.Roles.Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id
			});


            ; if (id == null || id == "")
			{
                 userVM = new()
                {
                    ApplicationUser = new ApplicationUser(),
                  RolesList=Roles

                };
                return PartialView(new UserVM());

            }
            string userRole = uof.AppUserRepo.GetRoleByUserId(id);
			 userVM = new()
			{
				ApplicationUser = uof.AppUserRepo.GetUserById(id),
				RolesList = Roles


			};
			userVM.ApplicationUser.Role = userRole;

            return PartialView(userVM);
	    }
		[HttpPost]
		public IActionResult RoleManagment(UserVM userVM)
		{
			string oldRole = uof.AppUserRepo.GetRoleByUserId(userVM.ApplicationUser.Id);
			string newRole = db.Roles.FirstOrDefault(u => u.Id == userVM.ApplicationUser.Role).Name;
			if (userVM.ApplicationUser.Role != oldRole)
			{
				//Role was updated
				ApplicationUser applicationUser = uof.AppUserRepo.GetUserById(userVM.ApplicationUser.Id);
				//if(newRole == StaticDetails.Manager_Role)
				//{
				//	applicationUser.CompanyId = userVM.ApplicationUser.CompanyId;
				//}
				//if(oldRole == StaticDetails.Company_Role)
				//{
				//	applicationUser.CompanyId = null;
				//}
				uof.Save();
				_userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
				
				_userManager.AddToRoleAsync(applicationUser, newRole).GetAwaiter().GetResult();
			}
			return RedirectToAction("Index");

			
		}



	}
}


