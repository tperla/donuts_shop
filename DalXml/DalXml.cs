using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
namespace Dal
{
    sealed internal class DalXml : IDal
    {
        public IProduct product { get; } = new Dal.DoProduct();
        public IOrder order { get; } = new Dal.DoOrder();
        public IOrderItem orderItem { get; } = new Dal.DoOrderItem();
        public IUsers users { get; } = new Dal.DoUsers();
        public static IDal Instance { get; } = new DalXml();
        private DalXml() { }//constractor
    }
}
