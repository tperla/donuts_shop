using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
namespace Dal;
/// <summary>
/// class for user's operations
/// </summary>
    internal class DoUsers : IUsers
    {
        readonly string s_rUser = "User";
    /// <summary>
    /// return the right user from the xml
    /// </summary>
    /// <param name="s"></param>
    /// <returns>DO.Users?</returns>
    static DO.Users? createUserfromXElement(XElement user)
    {
        DO.Enums.State st;
        Enum.TryParse((string?)(user.Element("state")), out st);
        //frate user:
        return new DO.Users()
        {
            password = (string?)user.Element("password"),
            UserName = (string?)user.Element("UserName"),
            state = st,
        };
    }
    /// add a new user
    /// </summary>
    /// <param name="user"></param>
    /// <exception cref="DO.DalAlreadyExistsIdException">in case the user already exists</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(Users user)
        {
        //List<Users?> listUsers = XMLTools.LoadListFromXMLSerializer<DO.Users>(s_rUser);
        //if (listUsers.Exists(x => x?.UserName == user.UserName))
        //{
        //    throw new DalAlreadyExsistExeption("user");
        //}
        //XMLTools.SaveListToXMLSerializer(listUsers, s_rUser);
        //listUsers.Add(user);
        XElement usersRootElem = XMLTools.LoadListFromXMLElement(s_rUser);
        //check if the user exists or not:
        XElement? myUser = (from us in usersRootElem.Elements()
                            where us.Element("UserName")?.ToString() == user.UserName
                            select us).FirstOrDefault();
        if (myUser != null)//it exists
            throw new DO.DalAlreadyExsistExeption( "User");
        //creat xml user 
        XElement userElem = new XElement("User",
                                   new XElement("UserName", user.UserName),
                                   new XElement("Password", user.password),
                                   new XElement("State", user.state)
                                   );
        //add xml user to the xml file
        usersRootElem.Add(userElem);
        //update the list
        XMLTools.SaveListToXMLElement(usersRootElem, s_rUser);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Users</returns>
    /// <exception cref="DalMissingIdException">the user doesn't exist</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Users GetByUser(string user)
    {    
        //List<Users?> listUsers = XMLTools.LoadListFromXMLSerializer<DO.Users>(s_rUser);
        //return listUsers.Find(x => x?.UserName == user) ?? throw new DalMissingIdException("user");
            XElement usersRootElem = XMLTools.LoadListFromXMLElement(s_rUser);
            //return the ridht user from the xml
            return (from u in usersRootElem?.Elements()
                    where (string?)u.Element("UserName") == user
                    select (DO.Users?)createUserfromXElement(u)).FirstOrDefault()
                    ?? throw new DalMissingIdException( "User");    
    }
    }

