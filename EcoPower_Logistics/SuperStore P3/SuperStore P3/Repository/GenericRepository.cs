using Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace EcoPower_Logistics.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SuperStoreContext _context;

        public GenericRepository(SuperStoreContext context)
        {
            _context = context;
        }

        public void AddItem(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }

        public void DeleteItem(int itemId)
        {
            var entity = _context.Set<T>().Find(itemId);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<T> GetAllOrders()
        {
            return _context.Set<T>().ToList();
        }

        public T GetItemById(int itemId)
        {
            return _context.Set<T>().Find(itemId);
        }

        public void UpdateItem(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
