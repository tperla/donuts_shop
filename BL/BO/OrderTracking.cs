namespace BO
{
    public class OrderTracking
    {
        /// <summary>
        /// The ID of the OrderTracking
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// status of the order
        /// </summary>
        public Enums.OrderStatus? status { get; set; }
        /// <summary>
        /// list of traking
        /// </summary>
       public List<Tuple<DateTime?, string?>>? Traking { get; set; }
        public override string? ToString()
        {
            return  this.ToStringProperty();
        }
    }
}
