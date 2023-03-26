using DO;
namespace DalApi
{
    public interface IUsers 
    {
         void Add(Users user);
        DO.Users GetByUser(string user);
    }
}
