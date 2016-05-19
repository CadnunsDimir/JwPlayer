using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JwPlayer.Utils
{
    public static class Extensions
    {
        public static int ToInt(this string obj)
        {
            int ret =0 ;
            int.TryParse(obj, out ret);
            return ret;
        }
        public static bool ContainsOnlyNumbers(this string obj)
        {
            //var regex = new Regex(@"^\d+$");
            //return regex.IsMatch(obj);
            int ret = 0;
            return int.TryParse(obj.Trim(), out ret);
            //return ret;
        }
    }
}
