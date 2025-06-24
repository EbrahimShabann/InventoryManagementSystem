namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        public IWareHouseRepository WareHouse { get; }

        void Save();
    }
}
