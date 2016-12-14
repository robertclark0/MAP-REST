using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAP_REST.DataAccess;
using MAP_REST.Models;

namespace MAP_REST.QueryBuilder
{
    public class Builder
    {
        public string BuildQueryString(dynamic queryObject, bool download = false)
        {
            bool aggregateFlag = queryObject.aggregation.enabled;
            bool paginationFlag = queryObject.pagination.enabled;

            QueryComponents components = BuildQueryComponents(queryObject);

            string query = String.Empty;
            var SQLTemplate = new QueryBuilder.SQLTemplate();

            if (download)
            {
                query = String.Format(SQLTemplate.Unlimited
                    , components.source
                    , components.filters
                    , components.ordering);
            }
            else if (aggregateFlag && paginationFlag)
            {
                query = String.Format(SQLTemplate.PaginatedGrouping
                    , components.selections
                    , components.source
                    , components.filters
                    , components.grouping
                    , components.ordering
                    , queryObject.pagination.page
                    , queryObject.pagination.range);
            }
            else if (aggregateFlag)
            {
                query = String.Format(SQLTemplate.Grouping
                    , components.selections
                    , components.source
                    , components.filters
                    , components.grouping
                    , components.ordering);
            }
            else if (paginationFlag)
            {
                query = String.Format(SQLTemplate.Pagination
                    , components.selections
                    , components.source
                    , components.filters
                    , components.ordering
                    , queryObject.pagination.page
                    , queryObject.pagination.range);
            }
            else
            {
                query = String.Format(SQLTemplate.Default
                    , components.selections
                    , components.source
                    , components.filters
                    , components.ordering);
            }
            return query;
        }

        //SELECTIONS
        private QueryComponents BuildQueryComponents(dynamic queryObject)
        {
            var components = new QueryComponents();

            components.selections = BuildSelections(queryObject);
            components.filters = BuildFilters(queryObject);
            components.grouping = BuildGrouping(queryObject);
            components.ordering = BuildOrder(queryObject);
            components.source = BuildDataSource(queryObject);

            return components;
        }
        private string Select(dynamic selection)
        {
            switch ((string)selection.aggregateFunctionValue)
            {
                case "count":
                    return String.Format("COUNT([{0}]) AS [{1}]", selection.dataValue.COLUMN_NAME, selection.alias);
                case "sum":
                    return String.Format("ROUND(SUM([{0}]), {2}) AS [{1}]", selection.dataValue.COLUMN_NAME, selection.alias, 2);
                case "avg":
                    return String.Format("ROUND(AVG([{0}]), {2}) AS [{1}]", selection.dataValue.COLUMN_NAME, selection.alias, 2);
                case "min":
                    return String.Format("MIN([{0}]) AS [{1}]", selection.dataValue.COLUMN_NAME, selection.alias);
                case "max":
                    return String.Format("MAX([{0}]) AS [{1}]", selection.dataValue.COLUMN_NAME, selection.alias);
                default:
                    return String.Format("[{0}] AS [{1}]", selection.dataValue.COLUMN_NAME, selection.alias);
            }

        }
        private string SelectPivot(dynamic selection)
        {
            switch ((string)selection.aggregateFunctionValue)
            {
                case "count":
                    return String.Format("SUM(CASE WHEN {0} THEN 1 ELSE 0 END) AS [{1}]", Operators(selection, selection.pivotValues), selection.alias);
                case "sum":
                    return String.Format("ROUND(SUM(CASE WHEN {0} THEN [{1}] ELSE 0 END),{2}) AS [{3}]", Operators(selection, selection.pivotValues), selection.dataValue.COLUMN_NAME, 2, selection.alias);
                case "avg":
                    return String.Format("ROUND(AVG(CASE WHEN {0} THEN [{1}] ELSE NULL END),{2}) AS [{3}]", Operators(selection, selection.pivotValues), selection.dataValue.COLUMN_NAME, 2, selection.alias);
                case "min":
                    return String.Format("MIN(CASE WHEN {0} THEN [{1}] ELSE NULL END) AS [{2}]", Operators(selection, selection.pivotValues), selection.dataValue.COLUMN_NAME, selection.alias);
                case "max":
                    return String.Format("MAX(CASE WHEN {0} THEN [{1}] ELSE 0 END) AS [{2}]", Operators(selection, selection.pivotValues), selection.dataValue.COLUMN_NAME, selection.alias);
                default:
                    return String.Empty;
            }
        }

        private string Operators(dynamic operationObject, dynamic operations)
        {
            var operators = new List<string>();
            foreach (dynamic operation in operations)
            {
                switch ((string)operation.operation)
                {
                    case "greater":
                        operators.Add(String.Format("[{0}] > {1}", operationObject.dataValue.COLUMN_NAME, OperatorValueType(operation.selectedValues[0], (string)operationObject.dataValue.DATA_TYPE)));
                        break;
                    case "less":
                        operators.Add(String.Format("[{0}] < {1}", operationObject.dataValue.COLUMN_NAME, OperatorValueType(operation.selectedValues[0], (string)operationObject.dataValue.DATA_TYPE)));
                        break;
                    case "greaterE":
                        operators.Add(String.Format("[{0}] >= {1}", operationObject.dataValue.COLUMN_NAME, OperatorValueType(operation.selectedValues[0], (string)operationObject.dataValue.DATA_TYPE)));
                        break;
                    case "lessE":
                        operators.Add(String.Format("[{0}] <= {1}", operationObject.dataValue.COLUMN_NAME, OperatorValueType(operation.selectedValues[0], (string)operationObject.dataValue.DATA_TYPE)));
                        break;
                    case "equal":
                        operators.Add(String.Format("[{0}] = {1}", operationObject.dataValue.COLUMN_NAME, OperatorValueType(operation.selectedValues[0], (string)operationObject.dataValue.DATA_TYPE)));
                        break;
                    case "in":
                        operators.Add(String.Format("[{0}] IN ({1})", operationObject.dataValue.COLUMN_NAME, OperatorValueJoin(operation.selectedValues, (string)operationObject.dataValue.DATA_TYPE)));
                        break;
                }
            }
            return String.Join(" AND ", operators);
        }
        private string OperatorValueType(dynamic operation, string type)
        {
            switch (type)
            {
                case "varchar":
                    return String.Format("'{0}'", operation);
                default:
                    return String.Format("{0}", operation);
            }
        }
        private string OperatorValueJoin(dynamic operationValues, string type)
        {
            var values = new List<string>();

            foreach (dynamic operation in operationValues)
            {
                values.Add(OperatorValueType(operation, type));
            }

            return String.Join(", ", values);
        }
        private string Order(dynamic selection, bool aggregate = false)
        {
            if (aggregate)
            {
                return String.Format("[{0}] {1}", selection.aggregation.alias, selection.order);
            }
            else
            {
                return String.Format("[{0}] {1}", selection.name, selection.order);
            }
        }


        //SELECTION
        private string BuildSelections(dynamic queryObject)
        {
            var querySelections = new List<string>();

            foreach (dynamic selection in queryObject.selections)
            {
                if ((bool)selection.pivot)
                {
                    querySelections.Add(SelectPivot(selection));
                }
                else
                {
                    querySelections.Add(Select(selection));
                }
            }
            return String.Join(", ", querySelections);
        }

        //GROUPING
        private string BuildGrouping(dynamic queryObject)
        {
            var queryGrouping = new List<string>();

            foreach (dynamic selection in queryObject.selections)
            {
                if (!(bool)selection.pivot && !(bool)selection.aggregateFunction)
                {
                    queryGrouping.Add((string)selection.dataValue.COLUMN_NAME);
                }
            }
            return String.Join(", ", queryGrouping);
        }

        //FILTERS
        private string BuildFilters(dynamic queryObject)
        {
            var queryFilters = new List<string>();

            //USER LEVEL SECURITY, GET PACT RESTRICTIONS AND ADD THEM TO FILTERS.

            foreach (dynamic filter in queryObject.filters)
            {
                queryFilters.Add(Operators(filter, filter.operations));
            }

            if (queryFilters.Count > 0)
            {
                return "WHERE " + String.Join(" AND ", queryFilters); ;
            }
            return String.Empty;
        }

        //ORDER
        private string BuildOrder(dynamic queryObject)
        {
            var orders = new List<string>();

            foreach (dynamic selection in queryObject.selections)
            {
                if ((bool)selection.order)
                {
                    orders.Add(String.Format("{0} {1}", selection.dataValue.COLUMN_NAME, selection.orderValue));
                }
            }

            if (orders.Count > 0)
            {
                return "ORDER BY " + String.Join(", ", orders); ;
            }

            return String.Empty;
        }

        //DATA SOURCE
        private string BuildDataSource(dynamic queryObject)
        {
            var db = new ConnectionDataContext();
            DataSource dataSource = db.getDataSource((string)queryObject.source.alias);

            return String.Format("[{0}].[dbo].[{1}]", dataSource.Catalog, dataSource.SourceName);
        }
    }
}