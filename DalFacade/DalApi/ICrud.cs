using DO;
namespace DalApi
{
    public  interface ICrud<T> where T : struct
    {
        public int Add(T obj);
        public void Update(T obj);
        public void Delete(int id );
        public T Get(int id);
        public T GetbyFilter(Func<T?, bool>? filter);
        public IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    }
}
