using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAP_REST.QueryBuilder
{
    public class QueryBuilder
    {
        public string BuildQueryString(dynamic queryOjbect)
        {
            return "";
        }

        private string[] BuildSelections(dynamic queryOjbect)
        {
            var querySelections = new string[queryOjbect.selections.length];
            bool aggregateFlag = queryOjbect.aggregation.enabled;

            foreach (dynamic selection in queryOjbect.selections)
            {
                if (aggregateFlag)
                {

                }
                else
                {
                    
                }
            }

            return querySelections;
        }
    }
}