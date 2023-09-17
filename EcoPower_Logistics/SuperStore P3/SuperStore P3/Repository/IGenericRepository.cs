namespace EcoPower_Logistics.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAllOrders();
        void AddItem(T item);
        void UpdateItem(T item);
        void DeleteItem(int item);
        T GetItemById(int item);
    }
}
