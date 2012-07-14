using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DataTablesHelper
{
    public class DataTableModelBinder<T> : DefaultModelBinder where T : new()
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var o = base.BindModel(controllerContext, bindingContext);
            var request = controllerContext.HttpContext.Request;
            var sortedCols = request.Form.AllKeys.Where(x => x.Contains("iSortCol"));

            var m2 = (DataTableRequest<T>)o;
            foreach (var s in sortedCols)
            {
                var index = request.Form.Get(s);
                var shortName = request.Form.Get("mDataProp_" + index);
                var longName = m2.ColumnInfos.Single(x => x.ShortName == shortName).EFName;
                var asc = request.Form.Get("sSortDir_" + s.Replace("iSortCol_", "")).Equals("asc");
                try
                {
                    m2.SortBy.Add(longName, asc); 
                }
                catch (Exception)
                {
                    
                    //
                }
              
            }
            return o;
        }
    }
}
