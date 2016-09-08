﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace MAP_REST.QueryBuilder
{
    public class SQLTemplate
    {
        private string _default =
            "SELECT TOP 1000                                                    " +
            "   {0}                                                             " +
            "FROM                                                               " +
            "   {1}                                                             " +
            "WHERE                                                              " +
            "   {2}                                                             " +
            "ORDER BY                                                           " +
            "   {4}                                                             " ;

        private string _grouping =
            "SELECT                                                             " +
            "   {0}                                                             " +
            "FROM                                                               " +
            "   {1}                                                             " +
            "WHERE                                                              " +
            "   {2}                                                             " +
            "GROUP BY                                                           " +
            "   {3}                                                             " +
            "ORDER BY                                                           " +
            "   {4}                                                             " ;
        
        string _pagination =
            "SELECT                                                             " +
            "   *                                                               " +
            "FROM                                                               " +
            "(                                                                  " +
            "   SELECT                                                          " +
            "       ROW_NUMBER() OVER ( ORDER BY {0}, %%physloc%% ) AS RowNum   " +
            "       ,*                                                          " +
            "   FROM                                                            " +
            "	    {1}                                                         " +
            "   WHERE                                                           " +
            "	    {2}                                                         " +
            ") AS                                                               " +
            "   RowConstrainedResult                                            " +
            "WHERE                                                              " +
            "   RowNum >= {3}                                                   " +
            "   AND RowNum <= {4}                                               " +
            "ORDER BY                                                           " +
            "   {5}                                                             " ;

        string _paginatedGrouping =
            "SELECT                                                             " +
            "   *                                                               " +
            "FROM                                                               " +
            "(                                                                  " +
	        "   SELECT                                                          " +
		    "       ROW_NUMBER() OVER ( ORDER BY {0} ) AS RowNum                " +
            "       ,*                                                          " +
	        "   FROM                                                            " +
	        "   (                                                               " +
		    "       SELECT                                                      " +
			"           {1}                                                     " +
		    "       FROM                                                        " +
			"           {2}                                                     " +
		    "       WHERE                                                       " +
			"           {3}                                                     " +
		    "       GROUP BY                                                    " +
			"           TmtParentName                                           " +
	        "   ) AS                                                            " +
		    "       InitialResult                                               " +
            ") AS                                                               " +
	        "   RowConstrainedResult                                            " +
            "WHERE                                                              " +
	        "   RowNum >= {4}                                                   " +
            "   AND RowNum <= {5}                                               " +
            "ORDER BY                                                           " +
	        "   RowNum                                                          " ;

        public string Default
        {
            get { return Regex.Replace(_default, @"\s+", " "); }
        }
        public string Grouping
        {
            get { return Regex.Replace(_grouping, @"\s+", " "); }
        }
        public string Pagination
        {
            get { return Regex.Replace(_pagination, @"\s+", " "); }
        }
        public string PaginatedGrouping
        {
            get { return Regex.Replace(_paginatedGrouping, @"\s+", " "); }
        }
    }
}