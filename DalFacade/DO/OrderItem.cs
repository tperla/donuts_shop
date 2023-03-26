namespace DO;
/// <summary>
/// Ordering an item - a department that takes care of the features and actions that link between ordering a product and the product you wish to order
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// The ID of orderItem
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// picture of the product
    /// </summary>
    public string? picture { get; set; }
    /// <summary>
    /// The ID of the product
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// Order Number
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// price of the product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// The quantity of products of a certain type in the order
    /// </summary>
    public int Amount { get; set; }
    public string? Name { get; set; }
    /// <summary>
    /// overrun function
    /// </summary>
    /// <returns></returns>
    public override string? ToString()
    {
        return this.ToStringProperty();
    }
}
