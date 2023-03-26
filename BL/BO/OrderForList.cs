namespace BO
{
    public class OrderForList
    {
        /// <summary>
        /// ID 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The name of the customer who ordered the product
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// The status of order
        /// </summary>
        public Enums.OrderStatus Status { get; set; }
        /// <summary>
        /// The amount of the Item 
        /// </summary>
        public int AmountOfItems { get; set; }
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
