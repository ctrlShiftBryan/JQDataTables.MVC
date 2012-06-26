using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTablesHelper
{
    public class JQDT : JQDataTableAttribute
    {
        private int _order;
        private string _viewName;
        private bool _hidden;
        private bool _searched;

        public JQDT(int order = int.MaxValue, string viewName = "Default", bool hidden = false, bool searched = true)
        {
            _order = order;
            _viewName = viewName;
            _hidden = hidden;
            _searched = searched;
        }

      
        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

       
        public string ViewName
        {
            get { return _viewName; }
            set { _viewName = value; }
        }

       
        public bool Hidden
        {
            get { return _hidden; }
            set { _hidden = value; }
        }

     
        public bool Searched
        {
            get { return _searched; }
            set { _searched = value; }
        }
    }
}
