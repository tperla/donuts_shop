using BlApi;
using BO;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
using IProduct = BlApi.IProduct;
namespace BlImplementation
{
    internal class Product : IProduct
    {
        //DalApi.IDal? dal = DalApi.Factory.Get();
        private static readonly DalApi.IDal dal = DalApi.Factory.Get()!;
        public int ID { get; private set; }

        /// <summary>
        /// func that add product to the stock 
        /// </summary>
        /// <param name="product"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddProduct(BO.Product? product)//remember to handle the case that the name is not in english
        {
            try
            {
                if (dal.product.GetAll().Where(x => x?.ID == product?.ID).Any() || dal.product.GetAll().Where(x => x?.Name == product?.Name).Any())
                    throw new BlAllredyExistExeption("this product is allredy exist");
                // create product
                DO.Product? doProduct = new DO.Product()
                {
                    Price = (int)product?.Price!,
                    Name = product.Name,
                    InStock = product.InStock,
                    Category = (DO.Enums.Category)product.Category!,
                    ID = product.ID,
                    picture=product?.picture?? @"\pictures\defult.PNG"
                };
                // add the new product to the list in DAL
                dal!.product.Add((DO.Product)doProduct);
            }
            catch (DO.DalInvalidInputException ex)
            {
                throw new BlInvalidInputException("one of the values is negativ", ex);// use just for a negativ value, from the do
            }
            catch(DO.DalAlreadyExsistExeption ex)
            {
                throw new BlInvalidInputException("Alredy exist", ex);// use just for a negativ value, from the do
            }
        }
        /// <summary>
        /// func that delete product by id from the list
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="BlCantDeleteOrderedItem"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteProduct(int id)
        {
            try
            {
                if (id < 0)
                    throw new BO.BlInvalidInputException("id");
                var list = dal!.orderItem.GetAll();
                var listToDel = list?.Where(x => x?.ID == id);
                if (listToDel!.Any())
                    throw new BO.BlInvalidInputException("product");
                dal?.product.Delete(id);
            }
            catch (DO.DalDoesNotExistExeption ex)
            {
                throw new BO.BlMissingEntityException("Product does not exist", ex.Message);
            }
        }
        /// <summary>
        /// func that show to the manager all the products
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.ProductForList?> GetListedProducts(Func<BO.ProductForList?, bool>? filter = null)
        {
            IEnumerable<ProductForList?> list = from DO.Product? doProduct in dal!.product.GetAll()
                                                select new BO.ProductForList
                                                {
                                                    ID = doProduct?.ID ?? throw new NullReferenceException("Missing ID"),
                                                    Name = doProduct?.Name ?? throw new NullReferenceException("Missing Name"),
                                                    Category = (BO.Enums.Category?)doProduct?.Category ?? throw new NullReferenceException("Missing Category"),//defrent then Efrat!!!!!!-Enums in the casting
                                                    Price = doProduct?.Price ?? 0,
                                                    picture = doProduct?.picture ?? @"\pictures\defult.PNG"
                                                };
            return filter is null ? list : list.Where(filter);
        }
        /// <summary>
        /// func that return product by id to menager
        /// </summary>
        /// <param name="IDdProduct"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Product? GetProduct(int IDdProduct)
        {
            DO.Product doProduct = dal!.product.Get(IDdProduct);//in case that we dont find the id in the get throw an exeption
            return new BO.Product()
            {
                ID = doProduct.ID,
                Name = doProduct.Name,
                Price = doProduct.Price,
                Category = (BO.Enums.Category)doProduct.Category!,
                InStock = doProduct.InStock,
                picture = doProduct.picture ?? @"\pictures\defult.PNG"
            };
        }
        /// <summary>
        /// function that return product by id  to customer
        /// </summary>
        /// <param name="IDdProduct"></param>
        /// <param name="cartCustomer"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.ProductItem? GetProduct(int IDdProduct, BO.Cart? cartCustomer)
        {
            DO.Product? doProduct = dal!.product.Get(IDdProduct);//in case that we dont find the id in the get throw an exeption
            return new BO.ProductItem()
            {
                ID = doProduct?.ID ?? throw new NullReferenceException("Missing ID"),
                Name = doProduct?.Name ?? throw new NullReferenceException("Missing Name"),
                Price = doProduct?.Price ?? 0,
                Category = (BO.Enums.Category?)doProduct?.Category ?? throw new NullReferenceException("Missing Category"),
                InStock = doProduct?.InStock ?? 0,
                //call to function that calculate the amount by id of product
                Amount = FindAmountHelpFunc(ID),
                picture = doProduct?.picture ?? @"\pictures\defult.PNG"
            };
        }
        //private function that calculate the amount by id of product
        private int FindAmountHelpFunc(int ID)
        {
            foreach (var item in dal!.orderItem.GetAll())
            {
                if (ID == item?.ProductID)//if found  this id
                    return (int)item?.Amount!;
            }
            return 0;
        }
        /// <summary>
        /// func that return all the products to customer
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.ProductItem?> GetProducts()
        {
            return from DO.Product? doProduct in dal!.product.GetAll()
                   select new BO.ProductItem
                   {
                       ID = doProduct?.ID ?? throw new NullReferenceException("Missing ID"),
                       Name = doProduct?.Name ?? throw new NullReferenceException("Missing Name"),
                       Category = (BO.Enums.Category?)doProduct?.Category ?? throw new NullReferenceException("Missing Category"),//defrent then Efrat!!!!!!-Enums in the casting
                       Price = doProduct?.Price ?? 0,
                       InStock = doProduct?.InStock ?? 0,
                       Amount = FindAmountHelpFunc(ID),
                       picture = doProduct?.picture ?? @"\pictures\defult.PNG"
                   };
        }
        /// <summary>
        /// func that  got product and update it
        /// </summary>
        /// <param name="product"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateProduct(BO.Product? product)
        {
            //if product is correct
            if (product?.Price >= 0 && product.ID > 0 && product.Name != "" && product.InStock >= 0)
            {
                DO.Product doProduct = new DO.Product()
                {
                    Price = product.Price,
                    Name = product.Name,
                    InStock = product.InStock,
                    Category = (DO.Enums.Category)product.Category!,
                    ID = product.ID,
                    picture=product?.picture ?? @"\pictures\defult.PNG"
                };
                dal!.product.Update(doProduct);
            }
            else
            {
                throw new BlInvalidInputException("inCorrect Product");
            }
        }
        /// <summary>
        /// return to the manager all the products by spesific category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ProductForList?> GetListedProductsByCategory(BO.Enums.Category? category)
        {
            return GetListedProducts(x => x?.Category == category);
        }
        /// <summary>
        /// popular products
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BO.BlNullPropertyException"></exception>
        /// <exception cref="BO.BlMissingEntityException"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.ProductForList?> PopularItems()
        {
            //creat a list of groups of items that appear in order, by ID
            var popGroup = from item in dal!.orderItem.GetAll()
                           group item by ((DO.OrderItem?)(item))?.ProductID into g
                           select new { id = g.Key, Items = g };
            //take the 10 that appear in the biggest amount of orders 
            popGroup = popGroup.OrderByDescending(x => x.Items.Count()).Take(10);
            //return the 10 popular items:
            try
            {
                return from item in popGroup
                       let prod = dal.product.Get(item?.id ?? throw new BO.BlInvalidInputException("Prodact ID is null"))
                       select new BO.ProductForList
                       {
                           ID = prod.ID,
                           Name = prod.Name,
                           Price = prod.Price,
                           Category = (BO.Enums.Category)prod.Category!,
                           picture = prod.picture ?? @"\pictures\defult.PNG"
                       };
            }
            catch (BO.DalMissingIdException ex)
            {
                throw new BO.BlMissingEntityException("Product does not exist", ex);
            }
        }
    }
}
