namespace BO
{
   public class Order
    {
        /// <summary>
        /// Order Number
        /// </summary>
        public int ID { get; set; }
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
        /// Order date of the product
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// The date the product was sent
        /// </summary>
        public DateTime? ShipDate { get; set; }
        /// <summary>
        /// The date of transfer of the product
        /// </summary>
        public DateTime? DeliveryrDate { get; set; }
        /// <summary>
        /// date od day that paid
        /// </summary>
        //public DateTime PaymentDate { get; set; }
        /// <summary>
        /// status of order
        /// </summary>
        public Enums.OrderStatus? Status { get; set; }
        /// <summary>
        /// Items of order
        /// </summary>
        public IEnumerable<OrderItem>? Items { get; set; }
        /// <summary>
        /// total price of order
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
