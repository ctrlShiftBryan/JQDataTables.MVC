using System;
using DataTablesHelper;

namespace DataTablesConcept.Meta
{
    public class Product
    {

        [JQDTChild(EFName = "ProductSubcategory.ProductCategory.Name")]
        public String ProductCategoryName { get; set; }


        [JQDTChild(EFName = "ProductSubcategory.Name")]
        public String ProductSubcategoryName { get; set; }

        [JQDT]
        public string Name { get; set; }

        [JQDT]
        public int ProductID { get; set; }

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