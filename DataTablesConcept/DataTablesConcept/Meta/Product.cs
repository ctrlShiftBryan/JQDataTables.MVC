using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataTablesHelper;

namespace DataTablesConcept.Meta
{
    public class Product
    {

        [JQDT]
        public int ProductID { get; set; }

        [JQDT]
        public string Name { get; set; }

        [JQDT]
        public string ProductNumber { get; set; }

        [JQDT]
        public string Size { get; set; }

        [JQDT]
        public decimal ListPrice { get; set; }

        [JQDT]
        public DateTime SellStartDate { get; set; }

        [JQDT(Order = 0)]
        public int Sort { get; set; }

        [JQDT]
        public decimal StandardCost { get; set; }

     

    }
}