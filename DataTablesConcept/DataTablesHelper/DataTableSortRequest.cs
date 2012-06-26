using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTablesHelper
{
    public class DataTableSortRequest
    {
    //direction	back
    //fromPosition	10
    //group	
    //id	321
    //toPosition	3
        public string direction { get; set; }
        public int fromPosition { get; set; }
        public string group { get; set; }
        public string id { get; set; }
        public int toPosition { get; set; }

    }
}
