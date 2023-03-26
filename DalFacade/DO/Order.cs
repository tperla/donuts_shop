namespace DO;
/// <summary>
/// Order - a department that takes care of the features and operations of the order in the store
/// </summary>
public struct Order
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
    /// overrun function
    /// </summary>
    /// <returns></returns>
    public override string? ToString()
    {
        return this.ToStringProperty();
    }
}
