using DO;
namespace BO
{
    public class User
    {
        #region GetAndSetOfUser
       
        /// <summary>
        /// User Name of the user
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// Password of the user
        /// </summary>
        public string? Password { get; set; }
        #endregion
        public override string? ToString()
        {
            return this.ToStringProperty();
        }
        public Enums.State? state { get; set; }
    }
}

