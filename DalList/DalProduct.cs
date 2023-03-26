using DalApi;
using DO;
namespace Dal;
using System.Runtime.CompilerServices;
internal class DalProduct:IProduct
{
    /// <summary>
    /// A function to add product, and return the ID
    /// </summary>
    /// <param name="pro"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Product product)
    {
        //insert new product to array
        if (product.Price < 0) 
            throw new DO.DalInvalidInputException("price is negativ");
        if (product.InStock < 0) 
            throw new DO.DalInvalidInputException("in stock is negativ");
        if (product.ID < 0) 
            throw new DO.DalInvalidInputException("id is negativ");
        if (DataSource._productList.FirstOrDefault(p => p?.ID == product.ID) != null)
            throw new DalAlreadyExsistExeption("Product");
        DataSource._productList.Add(product);
        return product.ID;
    }
    /// <summary>
    /// A function that delete the product
    /// </summary>
    /// <param name="pro"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int idProduct)
    {
            if (DataSource._productList.Exists(x=>x?.ID == idProduct))
            {
                //delete product
                DataSource._productList.Remove(DataSource._productList.Find(x => x?.ID == idProduct));
                return;
            }  
        //if this product does not exist in array
        throw new DO.DalDoesNotExistExeption("this product does not exist");
    }
    /// <summary>
    ///   //Returns a product by ID number
    /// </summary>
    /// <param name="pro"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(int id)
    {
        return DataSource._productList.Find(x => x?.ID == id) ??/*if this id does not exist in array*/ throw new DO.DalMissingIdException("this id does not exist");
    }
    /// <summary>
    ///     //return a list/array of all the products that in stock
    /// </summary>
    /// <param name="pro"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        if (filter != null)
            return DataSource._productList.Where(item => filter(item));
        return DataSource._productList.Select(item => (item));
    }
    public bool GetProduct(int iD)
    {
        throw new NotImplementedException();
    }
    /// <summary>
    ///get by filter
    /// </summary>
    /// <param name="pro"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product GetbyFilter(Func<Product?, bool>? filter)
    {
        return DataSource._productList.Find(item => filter!(item)) ?? throw new DO.DalDoesNotExistExeption("not found ");
    }
    /// <summary>
    /// A function that updats the product by the new parameter that we received
    /// </summary>
    /// <param name="pro"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Product product)
    {
        var p = DataSource._productList.FindIndex(x => x?.ID == product.ID);
        if (p > -1) 
        {            
            DataSource._productList[p] = product;
            return;
        }      
        //if this product does not exist in array
        throw new DO.DalDoesNotExistExeption("this product does not exist");
    }
}
