using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAP_REST.QueryBuilder
{
    public class Builder
    {
        public string BuildQueryString(dynamic queryOjbect)
        {
            bool aggregateFlag = queryOjbect.aggregation.enabled;
            bool paginationFlag = queryOjbect.pagination.enabled;

            string[] selections = BuildSelections(queryOjbect);

            string query = String.Empty;
            var SQLTemplate = new QueryBuilder.SQLTemplate();

            if (aggregateFlag && paginationFlag)
            {
                query = String.Format(SQLTemplate.PaginatedGrouping
                    , selections[0]
                    , queryOjbect.source.name
                    , "[Region] = 'RHC-A(P)'"
                    , selections[1]
                    , selections[2]
                    , queryOjbect.pagination.page
                    , queryOjbect.pagination.range);
            }
            else if (aggregateFlag)
            {
                query = String.Format(SQLTemplate.Grouping
                    , selections[0]
                    , queryOjbect.source.name
                    , "[Region] = 'RHC-A(P)'"
                    , selections[1]
                    , selections[2]);
            }
            else if (paginationFlag)
            {
                query = String.Format(SQLTemplate.Pagination
                    , selections[0]
                    , queryOjbect.source.name
                    , "[Region] = 'RHC-A(P)'"
                    , selections[2]
                    , queryOjbect.pagination.page
                    , queryOjbect.pagination.range);
            }
            else
            {
                query = String.Format(SQLTemplate.Default
                    , selections[0]
                    , queryOjbect.source.name
                    , "[Region] = 'RHC-A(P)'"
                    , selections[2]);
            }
            return query;
        }

        private string[] BuildSelections(dynamic queryOjbect)
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
                    if ((bool)selection.aggregate)
                    {
                        switch ((string)selection.aggregation.type)
                        {
                            case "count":
                                querySelections.Add(SelectCount(selection));
                                queryOrdering.Add(Order(selection, true));
                                break;
                            case "sum":
                                querySelections.Add(SelectSum(selection));
                                queryOrdering.Add(Order(selection, true));
                                break;
                            case "case-count":
                                querySelections.Add(SelectCaseCount(selection));
                                queryOrdering.Add(Order(selection, true));
                                break;
                            case "case-sum":
                                querySelections.Add(SelectCaseSum(selection));
                                queryOrdering.Add(Order(selection, true));
                                break;
                        }
                    }
                    else
                    {
                        querySelections.Add(Select(selection));
                        queryOrdering.Add(Order(selection));
                        queryGrouping.Add(Select(selection));
                    }
                }
                else
                {
                    if (!(bool)(selection.aggregate))
                    {
                        querySelections.Add(Select(selection));
                        queryOrdering.Add(Order(selection));
                    }
                }
            }

            var selections = String.Join(", ", querySelections);
            var grouping = String.Join(", ", queryGrouping);
            var ordering = String.Join(", ", queryOrdering);
            return new string[] { selections, grouping, ordering };
        }

        //SELECT
        private string Select(dynamic selection)
        {
            return String.Format("[{0}]", selection.name);
        }
        private string SelectCount(dynamic selection)
        {
            return String.Format("COUNT([{0}]) AS [{0}]", selection.aggregation.allias);
        }
        private string SelectSum(dynamic selection)
        {
            return String.Format("SUM([{0}]) AS [{0}]", selection.aggregation.allias);
        }
        private string SelectCaseCount(dynamic selection)
        {
            return String.Format("SUM(CASE WHEN {0} THEN 1 ELSE 0 END) AS [{1}]", aggregateOperators(selection), selection.aggregation.allias);
        }
        private string SelectCaseSum(dynamic selection)
        {
            return String.Format("ROUND(SUM(CASE WHEN {0} THEN [{1}] ELSE 0 END),{2}) AS [{3}]", aggregateOperators(selection), selection.name, selection.aggregation.round, selection.aggregation.allias);
        }
        private string aggregateOperators(dynamic selection)
        {
            var operators = new List<string>();
            foreach (dynamic operation in selection.aggregation.operators)
            {
                switch ((string)operation.type)
                {
                    case "greater":
                        operators.Add(String.Format("[{0}] > {1}", selection.name, operatorValueType(operation, 0)));
                        break;
                    case "less":
                        operators.Add(String.Format("[{0}] < {1}", selection.name, operatorValueType(operation, 0)));
                        break;
                    case "greaterEqual":
                        operators.Add(String.Format("[{0}] >= {1}", selection.name, operatorValueType(operation, 0)));
                        break;
                    case "lessEqual":
                        operators.Add(String.Format("[{0}] <= {1}", selection.name, operatorValueType(operation, 0)));
                        break;
                    case "equal":
                        operators.Add(String.Format("[{0}] = {1}", selection.name, operatorValueType(operation, 0)));
                        break;
                    case "in":
                        break;
                    case "between":
                        operators.Add(String.Format("[{0}] BETWEEN {1} AND {2}", selection.name, operatorValueType(operation, 0), operatorValueType(operation, 1)));
                        break;
                }
            }
            return String.Join(" AND ", operators);
        }
        private string operatorValueType(dynamic operation, int valueIndex)
        {
            switch ((string)operation.valueType)
            {
                case "string":
                    return String.Format("'{0}'", operation.values[valueIndex]);
                default:
                    return String.Format("{0}", operation.values[valueIndex]);
            }
        }

        //ORDER
        private string Order(dynamic selection, bool aggregate = false)
        {
            if (aggregate)
            {
                return String.Format("[{0}] {1}", selection.aggregation.allias, selection.order);
            }
            else
            {
                return String.Format("[{0}] {1}", selection.name, selection.order);
            }
        }
    }
}