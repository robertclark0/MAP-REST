using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAP_REST.SQL
{
    public class Repository
    {
        public string getReportList(string ProductLineID)
        {
            return  String.Format(
                    "SELECT [GUID],[User],[Report_Name],[Report_Type]" +
                    "FROM" +
	                    "[Report]" +
                    "WHERE" +
                        "ProductLineID = '{0}'"
                    , ProductLineID);
        }
    }
}