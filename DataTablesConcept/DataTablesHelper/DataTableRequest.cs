﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DataTablesHelper
{
    public class DataTableRequest<T> : DataTableBase<T> where T : new()
    {
        public DataTableRequest(T col, int? limit = null, string schema = "dbo")
            : base(col, limit)
        {
            SetDefaults(schema);
        }

        public DataTableRequest()
            : base(new T(), null)
        {
            SetDefaults("dbo");
        }

        private void SetDefaults(string schema)
        {
            SortBy = new Dictionary<string, bool>();
            Schema = schema;
        }

        public int iDisplayLength { get; set; }

        public int sEcho { get; set; }
        public int iDisplayStart { get; set; }

        public string sSearch { get; set; }


        public int iSortingCols { get; set; }

        public string iSortCol_0 { get; set; }

        public string mDataProp_0 { get; set; }
        public string mDataProp_1 { get; set; }
        public string mDataProp_2 { get; set; }

        public string[] mDataProp { get; set; }

        public Dictionary<string, bool> SortBy { get; set; }

        public string GetOrderClause(string alias = "")
        {


            var s = new StringBuilder();
            s.Append("|");
            foreach (var sb in SortBy)
            {
                s.Append(",");

                if (!String.IsNullOrEmpty(alias))
                {
                    s.Append(alias);
                    s.Append(".");
                }
                s.Append(sb.Key);
                s.Append(" ");
                s.Append(sb.Value ? "ASC" : "DESC");
            }

            return s.Replace("|,", "").Replace("|", "").ToString();

        }


        public string GetColumnsClause(string alias = "")
        {
            var sb = new StringBuilder();
            foreach (var ci in ColumnInfos)
            {
                sb.Append(",");

                if (!alias.Equals(""))
                {
                    sb.Append(alias);
                    sb.Append(".");

                }
                sb.Append(" [");
                sb.Append(ci.LongName);
                sb.Append("] ");
            }

            var result = sb.ToString();
            return result.Substring(1, result.Length - 1);
        }

        public string GetWhereSQLClause(string alias = "")
        {
            var sb = new StringBuilder();

            if (!String.IsNullOrEmpty(sSearch))
            {
                foreach (var ci in ColumnInfos)
                {
                    sb.Append("OR ");

                    if (!alias.Equals(""))
                    {
                        sb.Append(alias);
                        sb.Append(".");

                    }

                    sb.Append(" CONVERT(varchar, ");
                    sb.Append(ci.LongName);
                    sb.Append(" )");

                    sb.Append(" like '%");
                    sb.Append(sSearch);
                    sb.Append("%' ");
                }
                var result = sb.ToString();
                return " AND " + result.Substring(3, result.Length - 3);
            }
            return "";

        }



        public string GetTableName()
        {
            var s = typeof(T).Name;



            return Schema + "." + s;

        }

        public string GetWhereClause(string alias = "")
        {
            var sb = new StringBuilder();

            if (!String.IsNullOrEmpty(sSearch))
            {
                foreach (var ci in ColumnInfos)
                {

                    if (ci.IsString)
                    {
                        sb.Append(" || ");

                        sb.Append("");
                        sb.Append(ci.LongName);


                        //sb.Append(".ToString()");

                        sb.Append(".Contains(\"");

                        sb.Append(sSearch);
                        sb.Append("\")");
                    }

                }
                var result = sb.ToString();
                return result.Substring(4, result.Length - 4);
            }
            return "";

        }

        public string GetEntitySQLWhereClause(string alias = "")
        {
            var sb = new StringBuilder();

            if (!String.IsNullOrEmpty(sSearch) || ColumnInfos.Any(x => x.SearchTerm != ""))
            {

                if (!String.IsNullOrEmpty(sSearch))
                {
                    foreach (var ci in ColumnInfos)
                    {

                        if (ci.IsString)
                        {
                            sb.Append(" || ");

                            if (alias != "")
                            {
                                sb.Append(" ");
                                sb.Append(alias);
                                sb.Append(".");
                            }

                            sb.Append(
                                ci.EFName
                                );




                            sb.Append(" like '%");

                            sb.Append(sSearch);
                            sb.Append("%' ");
                        }
                        else
                        {
                            sb.Append(" || CAST(");



                            //CAST(p.Column as System.String)

                            if (alias != "")
                            {
                                sb.Append(" ");
                                sb.Append(alias);
                                sb.Append(".");
                            }


                            sb.Append(ci.LongName);

                            sb.Append(" as System.String)");


                            sb.Append(" like '%");

                            sb.Append(sSearch);
                            sb.Append("%' ");
                        }



                    }
                }

                foreach (var ci in ColumnInfos.Where(x => !String.IsNullOrEmpty(x.SearchTerm)))
                {
                    if (ci.IsString)
                    {
                        sb.Append(" && ");

                        if (alias != "")
                        {
                            sb.Append(" ");
                            sb.Append(alias);
                            sb.Append(".");
                        }

                        sb.Append(
                            ci.EFName
                            );




                        sb.Append(" like '%");

                        sb.Append(ci.SearchTerm);
                        sb.Append("%' ");
                    }
                    else
                    {
                        sb.Append(" && CAST(");



                        //CAST(p.Column as System.String)

                        if (alias != "")
                        {
                            sb.Append(" ");
                            sb.Append(alias);
                            sb.Append(".");
                        }


                        sb.Append(ci.LongName);

                        sb.Append(" as System.String)");


                        sb.Append(" like '%");

                        sb.Append(ci.SearchTerm);
                        sb.Append("%' ");
                    }
                }


                var result = sb.ToString();

                try
                {
                    var r2 = result.Substring(4, result.Length - 4);

                    return r2;
                }
                catch (Exception)
                {
                    
                   
                }
              
            }
            return "";

        }


        public string Schema { get; set; }

        public string SelectStatement
        {

            get
            {
                return String.Format(GetSQLTemplate

                    , this.iDisplayStart / this.iDisplayLength  //0 skip
                    , this.iDisplayLength//1 take
                    , this.GetTableName()//2 table name
                    , this.GetWhereSQLClause()//3 AND WHERE CLAUSE
                    , this.GetColumnsClause("x")//4 columns with 'x.' alias
                    , this.GetOrderClause("x")//5 order by clause with 'x.' alias
                    , this.GetColumnsClause()//6 columns with no alias
                    );
            }
        }

        public string GetSQLTemplate
        {
            get
            {

                //0 skip
                //1 take
                //2 table name
                //3 AND WHERE CLAUSE
                //4 columns with 'x.' alias
                //5 order by clause with 'x.' alias
                //6 columns with no alias

                const string sqltemplate = "DECLARE @skip INT; " +
                                           "DECLARE @take INT; " +
                                           "SET @skip = {0}; " +
                                           "SET @take = {1}; " +
                                           "DECLARE @StartRow INT; " +
                                           "DECLARE @EndRow INT; " +
                                           "SET @StartRow = @skip * @take; " +
                                           "SET @EndRow = @StartRow + @take; " +
                                           "DECLARE @total INT; " +
                                           "SELECT @total = COUNT(*) " +
                                           "FROM {2} x " +
                                           "WHERE 1 = 1 {3} ;" +
                                           "WITH cte AS " +
                                           "( " +
                                           "SELECT {4}, " +
                                           " ROW_NUMBER() OVER( " +
                                           "ORDER BY {5} ) AS RowNumber " +
                                           "FROM {2} x " +
                                           "WHERE 1 = 1 {3} " +
                                           ") " +
                                           "SELECT {6} , @total Total " +
                                           "FROM cte " +
                                           "WHERE RowNumber > @StartRow " +
                                           "AND RowNumber < @EndRow " +
                                           "ORDER BY RowNumber ";
                return sqltemplate;
            }
        }
        
        public string Table()
        {
            var plural = PluralizationService.CreateService(new CultureInfo("en-US"));
            var table = plural.Pluralize(typeof (T).Name);

            return table;
        }

        public string GetInitESQL()
        {

            return String.Format( "SELECT VALUE p FROM {0} p", Table());
        }


        public JQDataTablesWrapper<T> GetData(ObjectContext dbcontext)
        {

          

            

            var request = this;
            var entitySQL = String.Format("SELECT VALUE Count(1) FROM {0} AS x;", Table());


            var query = dbcontext.CreateQuery<Int32>(entitySQL);


            var total = query.First();


            //get query string without paging
            var orderBy = request.GetOrderClause("x");
            var top = request.iDisplayLength;
            var skip = request.iDisplayStart;
            var where = request.GetEntitySQLWhereClause("x");
            var queryStringBuilder = new StringBuilder();

            queryStringBuilder.Append("SELECT VALUE x ");

            //todo reflect DB.Products
            queryStringBuilder.Append(
                String.Format("FROM {0} AS x ", Table())
                );

            if (!where.Equals(""))
            {
                queryStringBuilder.Append("WHERE ");
                queryStringBuilder.Append(where);

            }

            queryStringBuilder.Append("ORDER BY ");
            queryStringBuilder.Append(orderBy);
            var queryString = queryStringBuilder.ToString();

            var products = dbcontext.CreateQuery<T>(queryString);

            //get unpaged count
            var filteredCount = products.Count();

            //page the products


            var productsPaged = dbcontext.CreateQuery<T>(queryString + " SKIP " + skip.ToString() + " LIMIT " + top.ToString());
            var list = productsPaged;
            var i = new JQDataTablesWrapper<T>(list, null, total, filteredCount);
            return i;
        }
    }
}
