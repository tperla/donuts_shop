using DO;
namespace DalApi
{
    public interface IProduct : ICrud<Product>
    {
      bool GetProduct(int iD);
    }
}
