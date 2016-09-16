using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAP_REST.QueryBuilder
{
    public class Builder
    {
        public string BuildQueryString(dynamic queryObject, bool download = false)
        {
            bool aggregateFlag = queryObject.aggregation.enabled;
            bool paginationFlag = queryObject.pagination.enabled;

            string[] selections = BuildSelections(queryObject);
            string filters = BuildFilters(queryObject);

            string query = String.Empty;
            var SQLTemplate = new QueryBuilder.SQLTemplate();

            if (download)
            {
                query = String.Format(SQLTemplate.Unlimited
                    , selections[0]
                    , queryObject.source.name
                    , filters
                    , selections[2]);
            }
            else if (aggregateFlag && paginationFlag)
            {
                query = String.Format(SQLTemplate.PaginatedGrouping
                    , selections[0]
                    , queryObject.source.name
                    , filters
                    , selections[1]
                    , selections[2]
                    , queryObject.pagination.page
                    , queryObject.pagination.range);
            }
            else if (aggregateFlag)
            {
                query = String.Format(SQLTemplate.Grouping
                    , selections[0]
                    , queryObject.source.name
                    , filters
                    , selections[1]
                    , selections[2]);
            }
            else if (paginationFlag)
            {
                query = String.Format(SQLTemplate.Pagination
                    , selections[0]
                    , queryObject.source.name
                    , filters
                    , selections[2]
                    , queryObject.pagination.page
                    , queryObject.pagination.range);
            }
            else
            {
                query = String.Format(SQLTemplate.Default
                    , selections[0]
                    , queryObject.source.name
                    , filters
                    , selections[2]);
            }
            return query;
        }

        //SELECTIONS
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
                                querySelections.Add(Select(selection, "count"));
                                queryOrdering.Add(Order(selection, true));
                                break;
                            case "sum":
                                querySelections.Add(Select(selection, "sum"));
                                queryOrdering.Add(Order(selection, true));
                                break;
                            case "case-count":
                                querySelections.Add(Select(selection, "case-count"));
                                queryOrdering.Add(Order(selection, true));
                                break;
                            case "case-sum":
                                querySelections.Add(Select(selection, "case-sum"));
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
        private string Select(dynamic selection, string format = null)
        {
            switch (format)
            {
                case "count":
                    return String.Format("COUNT([{0}]) AS [{0}]", selection.aggregation.allias);
                case "sum":
                    return String.Format("SUM([{0}]) AS [{0}]", selection.aggregation.allias);
                case "case-count":
                    return String.Format("SUM(CASE WHEN {0} THEN 1 ELSE 0 END) AS [{1}]", Operators(selection, selection.aggregation.operators), selection.aggregation.allias);
                case "case-sum":
                    return String.Format("ROUND(SUM(CASE WHEN {0} THEN [{1}] ELSE 0 END),{2}) AS [{3}]", Operators(selection, selection.aggregation.operators), selection.name, selection.aggregation.round, selection.aggregation.allias);
                default:
                    return String.Format("[{0}]", selection.name);
            }
            
        }
        private string Operators(dynamic operationObject, dynamic operations)
        {
            var operators = new List<string>();
            foreach (dynamic operation in operations)
            {
                switch ((string)operation.type)
                {
                    case "greater":
                        operators.Add(String.Format("[{0}] > {1}", operationObject.name, OperatorValueType(operation, 0)));
                        break;
                    case "less":
                        operators.Add(String.Format("[{0}] < {1}", operationObject.name, OperatorValueType(operation, 0)));
                        break;
                    case "greaterEqual":
                        operators.Add(String.Format("[{0}] >= {1}", operationObject.name, OperatorValueType(operation, 0)));
                        break;
                    case "lessEqual":
                        operators.Add(String.Format("[{0}] <= {1}", operationObject.name, OperatorValueType(operation, 0)));
                        break;
                    case "equal":
                        operators.Add(String.Format("[{0}] = {1}", operationObject.name, OperatorValueType(operation, 0)));
                        break;
                    case "in":
                        break;
                    case "between":
                        operators.Add(String.Format("[{0}] BETWEEN {1} AND {2}", operationObject.name, OperatorValueType(operation, 0), OperatorValueType(operation, 1)));
                        break;
                }
            }
            return String.Join(" AND ", operators);
        }
        private string OperatorValueType(dynamic operation, int valueIndex)
        {
            switch ((string)operation.valueType)
            {
                case "string":
                    return String.Format("'{0}'", operation.values[valueIndex]);
                default:
                    return String.Format("{0}", operation.values[valueIndex]);
            }
        }
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

        //FILTERS
        private string BuildFilters(dynamic queryObject)
        {
            var queryFilters = new List<string>();

            foreach (dynamic filter in queryObject.filters)
            {
                queryFilters.Add(Operators(filter, filter.operators));
            }

            return String.Join(" AND ", queryFilters); ;
        }
    }
}