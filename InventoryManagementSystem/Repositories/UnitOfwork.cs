using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Repositories
{
    public class UnitOfwork : IUnitOfWork
    {
        private readonly AppDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        IWareHouseRepo _wareHouseRepo;
        IInventoryItemRepo _inventoryItemRepo;
        ISupplierRepo _supplierRepo;
        IAppUserRepo userRepo;
        ICategoryRepo _categoryRepo;
        public UnitOfwork(AppDbContext db,UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public IWareHouseRepo warehouseRepo
        {
            get
            {
                if (_wareHouseRepo != null) return _wareHouseRepo;
                _wareHouseRepo = new WareHouseRepository(db,userManager);
                return _wareHouseRepo;
            }
        }
        public IInventoryItemRepo inventoryItemRepo
        {
            get
            {
                if (_inventoryItemRepo != null) return _inventoryItemRepo;
                _inventoryItemRepo = new InventoryItemRepository(db);
                return _inventoryItemRepo;
            }
        }
        public ISupplierRepo supplierRepo
        {
            get
            {
                if (_supplierRepo != null) return _supplierRepo;
                _supplierRepo = new SupplierRepository(db);
                return _supplierRepo;
            }
        }
        public IAppUserRepo AppUserRepo { get
            {
                if (userRepo != null) return userRepo;
                userRepo = new AppUserRepo(db);
                return userRepo ;
            } 
         }
        public ICategoryRepo categoryRepo
        {
            get
            {
                if (_categoryRepo != null) return _categoryRepo;
                _categoryRepo = new CategoryRepository(db);
                return _categoryRepo;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
