using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Users
    {
        /// <summary>
        /// The ID of the product
        /// </summary>
        public string? password { get; set; }
        /// <summary>
        /// Name of the product
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// Category of the product
        /// </summary>
        public Enums.State? state { get; set; }
        public override string? ToString()
        {
            return this.ToStringProperty();
        }
    }
}
