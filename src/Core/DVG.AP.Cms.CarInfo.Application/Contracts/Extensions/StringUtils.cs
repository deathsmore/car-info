using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Extensions
{
    public static class StringUtils
    {
        public static string RemoveSpecial(string s)
        {
            return Regex.Replace(s, "[`~!@#$%^&*()_|+-=?;:'\"<>{}[]\\/]", string.Empty);
        }
    }
}
