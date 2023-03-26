using DO;
namespace BO
{
    public class Product
    {
        #region GetAndSetOfProduct
        /// <summary>
        /// The ID of the product
        /// </summary>
        public int ID { get; set; } 
        /// <summary>
        /// Name of the product
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// price of the product
        /// </summary>
        public string? picture { get; set; }
        public double Price { get; set; }
        /// <summary>
        /// Category of the product
        /// </summary>
        public Enums.Category? Category { get; set; }
        /// <summary>
        /// Checking if the item is in stock
        /// </summary>
        public int InStock { get; set; }
        /// <summary>
        /// overrun function
        /// </summary>
        /// <returns></returns>
        #endregion
     public override string? ToString()
        {
            return this.ToStringProperty();
        }
    }
}
