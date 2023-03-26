namespace BO
{
    public class OrderItem
    {
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The ID of the product
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// price of the product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The quantity of products of a certain type in the order
        /// </summary>
        public int Amount { get; set; }
        ///  /// <summary>
        /// oName
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// pictur of the product
        /// </summary>
        public string? picture { get; set; }
        ///  /// <summary>
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
