namespace BO
{
    public class ProductItem
    {
        /// <summary>
        /// ID Number
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The name of the product
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The price of product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The category of product
        /// </summary>
        public Enums.Category? Category { get; set; }
        /// <summary>
        /// the amount of product 
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// if product is  instock
        /// </summary>
        public int InStock { get; set; }
        /// <summary>
        /// picture of the product
        /// </summary>  
        public string? picture { get; set; }
        /// <summary>
        /// overrun function
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            return this.ToStringProperty();
        }
    }
}

