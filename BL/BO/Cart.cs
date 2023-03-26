namespace BO
{
    public class Cart
    {
        /// <summary>
        /// Items Number
        /// </summary>
        public IEnumerable<OrderItem>? Items { get; set; }
        /// <summary>
        /// The name of the customer who ordered the product
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// The email of the customer who ordered the product
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// The address of the customer who ordered the product
        /// </summary>
        public string? CustomerAdress { get; set; }
        /// <summary>
        /// the final price
        /// </summary>
        public double TotalPrice { get; set; }
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
