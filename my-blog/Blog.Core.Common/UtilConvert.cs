using System;

namespace Blog.Core.Common
{
    public static class UtilConvert
    {
        public static int ObjToInt(this object val, int errorValue = 0)
        {
            if (val != null && val != DBNull.Value && int.TryParse(val.ToString(), out var reval))
                return reval;
            return errorValue;
        }

        public static double ObjToMoney(this object val, double errorValue = 0)
        {
            if (val != null && val != DBNull.Value && double.TryParse(val.ToString(), out var reval))
                return reval;
            return errorValue;
        }

        public static decimal ObjToDecimal(this object thisValue, decimal errorValue = 0)
        {
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out var reval))
                return reval;
            return errorValue;
        }

        public static DateTime ObjToDate(this object thisValue, DateTime errorValue =  default(DateTime))
        {
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out var reval))
                return reval;
            return errorValue;
        }
        
        public static bool ObjToBool(this object thisValue,bool errorValue = false)
        {
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out var reval))
                return reval;
            return errorValue;
        }
        
        public static string ObjToString(this object value, string errorValue = null)
        {
            return value != null ? value.ToString()?.Trim() : errorValue;
        }
    }
}