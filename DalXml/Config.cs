using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Dal;
/// <summary>
/// class to take care of the running number
/// </summary>
static internal class Config
{
    readonly static string s_config = "Configuration";
    /// <summary>
    /// returns the current number for order 
    /// </summary>
    /// <returns>int</returns>
    /// <exception cref="NullReferenceException">in case that the value is a null</exception>
    static internal int ReturnNextOrderId()
    {
        return XMLTools.ToIntNullable(XMLTools.LoadListFromXMLElement(s_config), "NextOrderId") ?? throw new NullReferenceException();
    }
    /// <summary>
    /// update the number for order (+1)
    /// </summary>
    /// <param name="num">the new number</param>
    static internal void UpdateOrderId(int num)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderId")?.SetValue(num.ToString());
        XMLTools.SaveListToXMLElement(root, s_config);
    }
    /// <summary>
    /// returns the current number for order item
    /// </summary>
    /// <returns>int</returns>
    /// <exception cref="NullReferenceException">in case that the value is a null</exception>
    static internal int ReturnNextOrderItemId()
    {
        return XMLTools.ToIntNullable(XMLTools.LoadListFromXMLElement(s_config), "NextOrderItem") ?? throw new NullReferenceException();
    }
    /// <summary>
    /// update the number for order (+1)
    /// </summary>
    /// <param name="num">the new number</param>
    static internal void UpdateOrderItemId(int num)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderItem")?.SetValue(num.ToString());
        XMLTools.SaveListToXMLElement(root, s_config);
    }
}
