namespace BO
{
    /// <summary>
    /// exeptions for dal
    /// </summary>
    public class DalDoesNotExistExeption : Exception
    {
        public DalDoesNotExistExeption() : base() { }
        public DalDoesNotExistExeption(string? message) : base(message) { }
    }
    public class DalInvalidInputException : Exception
    {
        public DalInvalidInputException(string? message) : base(message) { }
    }
    public class BlAllredyExistExeption : Exception
    {
        public BlAllredyExistExeption(string? message) : base(message) { }
    }
    public class BlInvalidInputException : Exception
    {
        public string? Entity;
        public BlInvalidInputException(string? ent) : base() { Entity = ent; }
        public BlInvalidInputException(string? Entity, Exception innerException) : base(Entity, innerException) { }
        public override string ToString() => $"invalid, {Entity}";
    }
        public class DalMissingIdException : Exception
    {
        public DalMissingIdException() : base() { }
        public DalMissingIdException(string? message) : base(message) { }
    }
    public class BlDidntShipedYet : Exception
    {
        public BlDidntShipedYet() : base() { }
        public BlDidntShipedYet(string? message) : base(message) { }
    }
    public class BlCantDeleteOrderedItem : Exception
    {
        public BlCantDeleteOrderedItem() : base() { }
        public BlCantDeleteOrderedItem(string? message) : base(message) { }
    }
    public class BlCantDeleteProduct : Exception
    {
        public BlCantDeleteProduct() : base() { }
        public BlCantDeleteProduct(string? message) : base(message) { }
    }
    public class BlAllredyDelivered : Exception
    {
        public BlAllredyDelivered() : base() { }
        public BlAllredyDelivered(string? message) : base(message) { }

    }
    public class BlIdDoNotExistException : Exception//if not exist
    {
        public BlIdDoNotExistException(string? message, Exception innerException) : base(message, innerException) { }
        public override string ToString() => base.ToString() + $" does not exist";
    }
    public class BlMissingEntityException : Exception
    {
        public BlMissingEntityException() : base() { }
        public BlMissingEntityException(string? message1, string? message2) : base() { }
        public BlMissingEntityException(string? message) : base(message) { }
        public BlMissingEntityException(string? message, Exception innerException) : base(message, innerException) { }
    }
    public class BlNotInStockException: Exception
    {
        public BlNotInStockException() : base() { }
        public BlNotInStockException(int id, string? name) : base() { }
    }
    public class DalAlreadyExsistExeption : Exception
    {
        public DalAlreadyExsistExeption(string? message) : base(message) { }
    }
    public class DalDataCorruptionExeption : Exception
    {
        public DalDataCorruptionExeption(string? message) : base(message) { }
    }
    public class BlDetailInvalidException : Exception
    {
        public BlDetailInvalidException() : base() { }
        public BlDetailInvalidException(string? message) : base(message) { }
    }
    public class BlDetailInCorrect : Exception
    {
        public BlDetailInCorrect() : base() { }
        public BlDetailInCorrect(string? message) : base(message) { }
    }
}
