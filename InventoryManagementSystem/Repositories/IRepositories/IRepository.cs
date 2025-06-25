namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int? id);
        void Add (T obj);
        void Update (T obj);
        void Delete (int? id);
        void Save();
    }
}
