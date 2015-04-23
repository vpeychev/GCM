using System;
using System.Linq;
using System.Text.RegularExpressions;

using GCM.DataAccess;

namespace GCM.BL
{
    internal static class Extenxions
    {

        #region String Extenssions

        /// <summary>
        /// Validate input data 
        /// </summary>
        /// <param name="value">input data</param>
        /// <returns>TRUE-data are in expected format; FALSE-otherwise</returns>
        public static bool IsValidInput(this string value)
        {
            bool result = Regex.IsMatch(value, @"^[a-zA-Z]*(,\d)+$");
            if (result)
            {
                DataAccess.EnumDayTime tmp;
                result = Enum.TryParse(value.Split(',')[0], true, out tmp);
            }

            return result;
        }

        #endregion                      //String Extenssions

    }
}
