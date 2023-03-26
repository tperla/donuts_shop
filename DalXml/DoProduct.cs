using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Dal
{
    internal class DoProduct : IProduct
    {
        readonly string s_rProduct = "Product";
        /// <summary>
        /// Add function to add a product to the list of products (in the xml files)
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        /// <exception cref="ExistingObjectDo"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(Product product)
        {
            //insert new product to list
            List<Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_rProduct);
            if (product.Price < 0)
                throw new DO.DalInvalidInputException("price is negativ");
            if (product.InStock < 0)
                throw new DO.DalInvalidInputException("in stock is negativ");
            if (product.ID < 0)
                throw new DO.DalInvalidInputException("id is negativ");
            if (listProducts.FirstOrDefault(p => p?.ID == product.ID) != null)
                throw new DalAlreadyExsistExeption("Product");
            listProducts.Add(product);
            XMLTools.SaveListToXMLSerializer(listProducts, s_rProduct);
            return product.ID;
        }
        /// <summary>
        /// Delete function to remove a product from the list
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="NonFoundObjectDo"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id)
        {
            List<Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_rProduct);
            if (listProducts.Exists(x => x?.ID == id))
            {
                //delete product
                listProducts.Remove(listProducts.Find(x => x?.ID == id));
                XMLTools.SaveListToXMLSerializer(listProducts, s_rProduct);
                return;
            }
            //if this product does not exist in array
            throw new DO.DalDoesNotExistExeption("this product does not exist");
        }
        /// <summary>
        /// get function to return a product from the list
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="NonFoundObjectDo"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Product Get(int id)
        {
            List<Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_rProduct);
            return listProducts.Find(x => x?.ID == id) ??/*if this id does not exist in array*/ throw new DO.DalMissingIdException("this id does not exist");
        }
        /// <summary>
        /// get all function to return  all the list
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="NonFoundObjectDo"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
        {
            List<Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_rProduct);
            if (filter != null)
                return listProducts.Where(item => filter(item));
            return listProducts.Select(item => (item));
        }
        /// <summary>
        /// get by filter function 
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="NonFoundObjectDo"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Product GetbyFilter(Func<Product?, bool>? filter)
        {
            List<Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_rProduct);
            return listProducts.Find(item => filter!(item)) ?? throw new DO.DalDoesNotExistExeption("not found "); 
        }
        public bool GetProduct(int iD)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// update function to update a product from the list
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="NonFoundObjectDo"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(Product product)
        {
            List<Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_rProduct);
            var p = listProducts.FindIndex(x => x?.ID == product.ID);
            if (p > -1)
            {
                listProducts[p] = product;
                XMLTools.SaveListToXMLSerializer(listProducts, s_rProduct);
                return;
            }
            //if this product does not exist in array
            throw new DO.DalDoesNotExistExeption("this product does not exist");
        }
    }
}
