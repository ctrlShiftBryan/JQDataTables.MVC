using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DataTablesConcept.Models;
using DataTablesHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Dynamic;

namespace DataTablesConcept.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            

            //to do init table wrapper without list
            IEnumerable<Product> list = new DB().Products.ToList();
            var i = new JQDataTablesWrapper<Product>(list);

            return View(i);
        }

        public ActionResult Data(DataTableRequest<Product> request)
        {


            var dbcontext = new DB();

            //get total
            var total = dbcontext.Products.Count();
            
            //get query string without paging
            var orderBy = request.GetOrderClause("p");
            var top = request.iDisplayLength;
            var skip = request.iDisplayStart;
            var where = request.GetEntitySQLWhereClause("p");
            var queryStringBuilder = new StringBuilder();

            queryStringBuilder.Append("SELECT VALUE p ");

            //todo reflect DB.Products
            queryStringBuilder.Append("FROM DB.Products AS p ");
            
            if(!where.Equals(""))
            {
                queryStringBuilder.Append("WHERE ");
                queryStringBuilder.Append(where);

            }

            queryStringBuilder.Append("ORDER BY ");
            queryStringBuilder.Append(orderBy);
            var queryString = queryStringBuilder.ToString();

            var products = dbcontext.CreateQuery<Product>(queryString);
            
            //get unpaged count
            var filteredCount = products.Count();
            
            //page the products
           

            var productsPaged =  dbcontext.CreateQuery<Product>(queryString + " SKIP " + skip.ToString() + " LIMIT " + top.ToString());
            var list = productsPaged;
            var i = new JQDataTablesWrapper<Product>(list, null, total, filteredCount);

            return Content(i.DataTableInitJson(request));
        }

        public ActionResult Sort (DataTableSortRequest request)
        {

            //d from 2 to 4 direction = forward


            //update id set sort = to position

            //UPDATE TABLE
            //SET Sort = @to
            //WHERE ID = @id

            //-forward
            //UPDATE TABLE
            //SET Sort = Sort + 1
            //WHERE SORT >= @to AND ID <> @id AND SORT <= @from

            //-backward
            //UPDATE TABLE
            //SET Sort = Sort + 1
            //WHERE SORT <= @to AND ID <> @id AND SORT >= @from

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            return View();
        }




    }
}
