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
	[Authorize(Roles = StaticDetails.Admin_Role)]
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

           var UsersList =uof.AppUserRepo.sort("");
            
            var paged = UsersList.ToPagedResult(page, size);
          
            return View(paged);
        }
        public IActionResult search(string searchText, int page = 1, int size = 10)
        {
            var users = uof.AppUserRepo.sort("");
            dynamic pagedResult;
            if (!string.IsNullOrEmpty(searchText))
            {
                users = users.Where(w => w.UserName.Contains(searchText) || w.Address.Contains(searchText)).ToList();
                pagedResult = users.ToPagedResult(page, size);
                return PartialView("sortTable", pagedResult);
            }
            pagedResult = users.ToPagedResult(page, size);
            return PartialView("sortTable", pagedResult);


        }
        public IActionResult sortTable(string sortOrder, int page = 1, int size = 10)
        {
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["emailSortParam"] = sortOrder == "email" ? "email_desc" : "email";
            ViewData["phoneSortParam"] = sortOrder == "phone" ? "phone_desc" : "phone";
            ViewData["addressSortParam"] = sortOrder == "address" ? "address_desc" : "address";
            ViewData["roleSortParam"] = sortOrder == "role" ? "role_desc" : "role";

            var users = uof.AppUserRepo.sort(sortOrder).AsEnumerable();
            ViewBag.sortOrder = sortOrder;
            var pagedResult = users.ToPagedResult(page, size);
            return PartialView("sortTable", pagedResult);

        }
        public IActionResult UpSert(string id)
		{
			UserVM userVM;

			var Roles = db.Roles.Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id
			});


            if (id == null || id == "")
			{
                 userVM = new()
                {
                    ApplicationUser = new ApplicationUser(),
                  RolesList=Roles

                };
                return PartialView(userVM);

            }
            else
            {
                string userRole = uof.AppUserRepo.GetRoleOfUser(id);
                userVM = new()
                {
                    ApplicationUser = uof.AppUserRepo.GetUserById(id),
                    RolesList = Roles


                };
                userVM.UserName= userVM.ApplicationUser.UserName ;
                userVM.Email = userVM.ApplicationUser.Email;
                userVM.PhoneNumber = userVM.ApplicationUser.PhoneNumber;
                userVM.ApplicationUser.Role = userRole;
               
                return PartialView(userVM);
            }
              
	    }
		[HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> UpSert(UserVM userVM,IFormFile file)
		{
            try
            {


                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string FinalPath = Path.Combine("wwwroot", "images", fileName);
                        userVM.ApplicationUser.Image = fileName;
                        using (var stream = new FileStream(FinalPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                    var userFromDb = uof.AppUserRepo.GetUserById(userVM.ApplicationUser.Id);
                    if (userFromDb == null)
                    {
                        //not existed in db means creating new one
                        userVM.ApplicationUser.UserName = userVM.UserName;
                        userVM.ApplicationUser.Email = userVM.Email;
                        userVM.ApplicationUser.PhoneNumber = userVM.PhoneNumber;
                        userVM.ApplicationUser.EmailConfirmed = true;
                        string roleName = uof.AppUserRepo.GetRoleName(userVM.ApplicationUser.Role);
                        string password = userVM.ApplicationUser.UserName + "A253@"; //  create password from username + A253@ ex ahmedA253@ || managerA253@

                        await _userManager.CreateAsync(userVM.ApplicationUser, password);
                        await _userManager.AddToRoleAsync(userVM.ApplicationUser,roleName);
                    }
                    else
                    {
                        //user existed and try to update it
                        string oldRole = uof.AppUserRepo.GetRoleOfUser(userVM.ApplicationUser.Id);
                        string newRole = uof.AppUserRepo.GetRoleName(userVM.ApplicationUser.Role);
                        if (newRole != oldRole)
                        {
                            //Role was updated
                            if (oldRole != null)
                            {
                                await _userManager.RemoveFromRoleAsync(userFromDb, oldRole);

                            }
                            await _userManager.AddToRoleAsync(userFromDb, newRole);

                        }
                        if (userVM.ApplicationUser.Image != null)
                        {
                            userFromDb.Image = userVM.ApplicationUser.Image;

                        }
                        await _userManager.UpdateAsync(userFromDb);
                        //uof.AppUserRepo.Update(userFromDb);
                        //uof.Save();

                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("Ex", ex.Message);
            
            }
            return PartialView(userVM);
			
		}


        public IActionResult Delete(string id)
        {
            var user = uof.AppUserRepo.GetUserById(id);
            
            if (user != null)
            {

                uof.AppUserRepo.DeleteUser(user);
                uof.Save();
                return RedirectToAction("Index");
            }
            return PartialView("Error");
        }

        public IActionResult Details(string id)
        {
            var user = uof.AppUserRepo.GetUserById(id);
            if (user != null)
            {
                return View(user);
            }
            return View("Error");
        }
	}
}


