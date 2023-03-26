using BO;
namespace BlApi
{
    public interface IProduct
    {
        /// <summary>
        /// popular products
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.ProductForList?> PopularItems();
        /// <summary>
        /// Catalog to customer
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductItem?> GetProducts();
        /// <summary>
        /// Catalog to menagger
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductForList?> GetListedProducts(Func<BO.ProductForList?,bool>?filter=null);
        /// <summary>
        /// return product to menager
        /// </summary>
        /// <param name="IDdProduct"></param>
        /// <returns></returns>
        public Product? GetProduct(int IDdProduct);
        /// <summary>
        /// retirn product to customer
        /// </summary>
        /// <param name="IDdProduct"></param>
        /// <param name="cartCustomer"></param>
        /// <returns></returns>
        public  ProductItem? GetProduct(int IDdProduct,Cart? cartCustomer);
        /// <summary>
        /// add product 
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product? product);//Exeption or void
        /// <summary>
        /// daelte exist product
        /// </summary>
        /// <param name="IdProduct"></param>
        public void DeleteProduct(int IdProduct);
        /// <summary>
        /// update exist product
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(Product? product);
        /// <summary>
        /// return to the manager all the products by spesific category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public IEnumerable<ProductForList?> GetListedProductsByCategory(BO.Enums.Category? category);
    }
}
