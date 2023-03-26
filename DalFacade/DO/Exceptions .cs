namespace DO
{
    /// <summary>
    /// exeptions for dal
    /// </summary>
    /// 
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }
    public class DalDoesNotExistExeption:Exception
    {
        public DalDoesNotExistExeption(string? message) : base(message) { }
    }
    public class DalInvalidInputException : Exception
    {
        public DalInvalidInputException(string? message) : base(message) { }
    }
    public class DalAlreadyExsistExeption : Exception
    {
        public DalAlreadyExsistExeption(string? message) : base(message) { }
    }
    public class DalIdDoNotExistException : Exception//if not exist
    {
        public int EntityId;
        public string EntityName;
        public DalIdDoNotExistException(int id, string EName) : base() { EntityId = id; EntityName = EName; }
        public DalIdDoNotExistException(int id, string EName, string? message) : base(message) { EntityId = id; EntityName = EName; }
        public DalIdDoNotExistException(int id, string EName, string? message, Exception innerException) : base(message, innerException) { EntityId = id; EntityName = EName; }
        public override string ToString() => $"Id:{EntityId} of type {EntityName},does not exist";
    }
        public class DalDataCorruptionExeption : Exception
    {
        public DalDataCorruptionExeption(string? message) : base(message) { }
    }
    public class DalMissingIdException : Exception
    {
        public DalMissingIdException() : base() { }
        public DalMissingIdException(string? message) : base(message) { }
    }
}
