using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlApi
{
    public interface IUser
    {
        public void Add(BO.User user);
      public BO.User GetByUser(string user);
    }
}
