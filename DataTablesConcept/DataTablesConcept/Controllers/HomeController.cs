﻿using System;
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


            ObjectContext dbcontext = new DB();
            dbcontext.CreateQuery<Product>("SELECT VALUE p FROM Db.Products As p");
            JQDataTablesWrapper<Product> i2 = request.GetData(dbcontext);
            return Content(i2.DataTableInitJson(request));
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
