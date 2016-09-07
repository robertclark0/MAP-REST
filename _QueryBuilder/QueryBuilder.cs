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

        private string[] BuildQuery(dynamic queryOjbect)
        {
            var querySelections = new List<string>();
            var queryGrouping = new List<string>();
            var queryOrdering = new List<string>();

            bool aggregateFlag = queryOjbect.aggregation.enabled;
            bool paginationFlag = queryOjbect.pagination.enabled;

            foreach (dynamic selection in queryOjbect.selections)
            {
                if (aggregateFlag)
                {
                    if (selection.aggregate)
                    {
                        switch ((string)selection.aggregate.type)
                        {
                            case "count":
                                querySelections.Add(SelectCount(selection));
                                break;
                            case "sum":
                                querySelections.Add(SelectSum(selection));
                                break;
                            case "case-count":
                                querySelections.Add(SelectCaseCount(selection));
                                break;
                            case "case-sum":
                                querySelections.Add(SelectCaseSum(selection));
                                break;
                        }
                    }
                    else
                    {
                        querySelections.Add(Select(selection));
                        queryOrdering.Add(Order(selection));
                    }
                }
                else
                {
                    querySelections.Add(Select(selection));
                    queryOrdering.Add(Order(selection));
                }
            }

            return querySelections.ToArray();
        }

        private string Select(dynamic selection)
        {
            return String.Format("[{0}]", selection.name);
        }
        private string SelectCount(dynamic selection)
        {
            return String.Format("COUNT([{0}]) AS [{0}]", selection.aggregate.allias);
        }
        private string SelectSum(dynamic selection)
        {
            return String.Format("SUM([{0}]) AS [{0}]", selection.aggregate.allias);
        }
        private string SelectCaseCount(dynamic selection)
        {
            return String.Format("SUM(CASE WHEN {0} THEN 1 ELSE 0 END) AS [{1}]", aggregateOperators(selection), selection.aggregate.allias);
        }
        private string SelectCaseSum(dynamic selection)
        {
            return String.Format("ROUND(SUM(CASE WHEN {0} THEN [{1}] ELSE 0 END),{2}) AS [{3}]", aggregateOperators(selection), selection.name, selection.aggregate.round, selection.aggregate.allias);
        }
        private string aggregateOperators(dynamic selection)
        {
            var operators = new List<string>();
            foreach (dynamic operation in selection.aggregate.operators)
            {

            }
            return "";
        }

        private string Order(dynamic selection)
        {
            return String.Format("{0} {1}", selection.name, selection.order);
        }
    }
}