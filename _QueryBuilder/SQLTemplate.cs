using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace MAP_REST.QueryBuilder
{
    public class SQLTemplate
    {
        private string _unlimited =
            "SELECT *                                                           " +
            "FROM                                                               " +
            "   {0}                                                             " +
            "{1}                                                                " +
            "ORDER BY                                                           " + 
            "   {2}                                                             ";

        private string _default =
            "SELECT TOP 1000                                                    " +
            "   {0}                                                             " +
            "FROM                                                               " +
            "   {1}                                                             " +
            "{2}                                                                " +
            "ORDER BY                                                           " + 
            "   {3}                                                             " ;

        private string _grouping =
            "SELECT                                                             " +
            "   {0}                                                             " +
            "FROM                                                               " +
            "   {1}                                                             " +
            "{2}                                                                " +
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
            "       ROW_NUMBER() OVER ( ORDER BY {3}, %%physloc%% ) AS RowNum   " +
            "       ,{0}                                                        " +
            "   FROM                                                            " +
            "	    {1}                                                         " +
            "	{2}                                                             " +
            ") AS                                                               " +
            "   RowConstrainedResult                                            " +
            "WHERE                                                              " +
            "   RowNum >= {4}                                                   " +
            "   AND RowNum <= {5}                                               " +
            "ORDER BY                                                           " +
            "   RowNum                                                          " ;

        string _paginatedGrouping =
            "SELECT                                                             " +
            "   *                                                               " +
            "FROM                                                               " +
            "(                                                                  " +
	        "   SELECT                                                          " +
		    "       ROW_NUMBER() OVER ( ORDER BY {4} ) AS RowNum                         " +
            "       ,*                                                          " +
	        "   FROM                                                            " +
	        "   (                                                               " +
		    "       SELECT                                                      " +
			"           {0}                                                     " +
		    "       FROM                                                        " +
			"           {1}                                                     " +
			"       {2}                                                         " +
		    "       GROUP BY                                                    " +
			"           {3}                                                     " +
	        "   ) AS                                                            " +
		    "       InitialResult                                               " +
            ") AS                                                               " +
	        "   RowConstrainedResult                                            " +
            "WHERE                                                              " +
	        "   RowNum >= {5}                                                   " +
            "   AND RowNum <= {6}                                               " +
            "ORDER BY                                                           " +
	        "   RowNum                                                          " ;

        string _calculation =
            "SELECT                                                             " +
            "   {0}                                                             " +
            "   {1}                                                             " +
            "FROM                                                               " +
            "   (                                                               " +
            "      SELECT                                                       " +
            "         {2}                                                       " +
            "      FROM                                                         " +
            "         {3}                                                       " +
            "      {4}                                                          " +
            "      GROUP BY                                                     " +
            "         {5}                                                       " +
            "   ) AS InitialResult                                              " +
            "   {6}                                                             " +
            "ORDER BY                                                           " +
            "   {7}                                                             ";

        string _paginatedCalculation =
            "SELECT                                                             " +
            "   *                                                               " +
            "FROM                                                               " +
            "(                                                                  " +
            "	SELECT                                                          " +
            "		ROW_NUMBER() OVER ( ORDER BY {0} ) AS RowNum, *             " +
            "	FROM                                                            " +
            "	(                                                               " +
            "		SELECT                                                      " +
            "			{1}                                                     " +
            "			{2}                                                     " +
            "		FROM                                                        " +
            "			(                                                       " +
            "				SELECT                                              " +
            "					{3}                                             " +
            "				FROM                                                " +
            "					{4}                                             " +
            "				{5}                                                 " +
            "				GROUP BY                                            " +
            "					{6}                                             " +
            "			) AS                                                    " +
            "            InitialResult                                          " +
            "			{7}                                                     " +
            "	) AS                                                            " +
            "		CalculatedResult                                            " +
            ") AS                                                               " +
            "	RowConstrainedResult                                            " +
            "WHERE                                                              " +
            "	RowNum >= {8} AND                                               " +
            "	RowNum <= {9}                                                   " +
            "ORDER BY                                                           " +
            "	RowNum                                                          ";
	


        public string Default
        {
            get { return Regex.Replace(_default, @"\s+", " "); }
        }
        public string Unlimited
        {
            get { return Regex.Replace(_unlimited, @"\s+", " "); }
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
        public string Calculation
        {
            get { return Regex.Replace(_calculation, @"\s+", " "); }
        }
        public string PaginatedCalculation
        {
            get { return Regex.Replace(_paginatedCalculation, @"\s+", " "); }
        }
    }
}