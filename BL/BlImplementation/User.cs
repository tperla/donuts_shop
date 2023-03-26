using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BlImplementation
{
    internal class User:IUser
    {
        private static readonly DalApi.IDal dal = DalApi.Factory.Get()!;
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(BO.User user)
        {
            if (user.Password == null || user.Password == "")
            {
                throw new BO.BlDetailInvalidException("password");
            }
            if (user.UserName == null || user.UserName == "")
            {
                throw new BO.BlDetailInvalidException("userName");
            }
            DO.Users user1 = new DO.Users
            {
                UserName = user.UserName ?? " ",
                password = user.Password,
                state = (DO.Enums.State)user.state!,
            };
            try
            {
                dal.users.Add(user1);
            }
            catch(DO.DalAlreadyExsistExeption ex)
            {
                throw new BO.BlInvalidInputException("this user already exist",ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.User GetByUser(string user="")
        {
            if(user == null||user=="")
            {
                throw new BO.BlDetailInvalidException("userName");
            }
            DO.Users usernew =new DO.Users();
            try
            {
                usernew = dal.users.GetByUser(user);
            }
            catch( DO.DalMissingIdException ex)
            {
                throw new BO.BlMissingEntityException("user did not found", ex);
            }
            return new BO.User
            {
                UserName = usernew.UserName,   
                state=(BO.Enums.State)usernew.state!,
                Password= usernew.password,
            };
        }
    }
}
