namespace DO;
/// <summary>
/// Product - a department responsible for the features and operations of the products
/// </summary>
public struct Product
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
    /// picture of the product
    /// </summary>
    public string? picture { get; set; }
    /// <summary>
    /// price of the product
    /// </summary>
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
