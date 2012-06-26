using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
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
            


            IEnumerable<Product> list = new DB().Products.ToList();


          
            var i = new JQDataTablesWrapper<Product>(list);

            return View(i);
        }

        public ActionResult Data(DataTableRequest<Product> request)
        {
           // request.Limit = 5;
            request.Schema = "Production";
            var select = request.SelectStatement;

            var total = new DB().Products.Count();
            var products = new DB().Products.Where(x => x.Name.Contains(request.sSearch) || String.IsNullOrEmpty(request.sSearch));
            var filteredCount = products.Count();

            IEnumerable<Product> list = products
                .OrderBy(request.GetOrderClause())
                .Skip(request.iDisplayStart)
                .Take(request.iDisplayLength);
            
            var i = new JQDataTablesWrapper<Product>(list,null,total,filteredCount);
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
