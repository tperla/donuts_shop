using DalApi;
using DO;
namespace Dal;
using System.Runtime.CompilerServices;
internal class DalUsers :IUsers
{
    /// <summary>
    /// add user to list
    /// </summary>
    /// <param name="user"></param>
    /// <exception cref="DalAlreadyExsistExeption"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(Users user)
    {
        if (DataSource._usersList.Exists(x => x?.UserName == user.UserName))
        {
            throw new DalAlreadyExsistExeption("user");
        }
        DataSource._usersList.Add(user);
    }
    /// <summary>
    /// return user by user name
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="DalMissingIdException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Users GetByUser(string user="")
    {
        return DataSource._usersList.Find(x => x?.UserName == user)??throw new DalMissingIdException("user");
    }
}
