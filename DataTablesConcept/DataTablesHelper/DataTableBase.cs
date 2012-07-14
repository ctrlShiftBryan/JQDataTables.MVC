using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace DataTablesHelper
{
    public class DataTableBase<T>
    {
        private readonly Regex rgx = new Regex("[^A-Z]");

     
        public DataTableBase(T col, int? limit = null)
        {

            _abbreviationCount = new Dictionary<string, int>();
            _limit = limit.HasValue ? limit.Value : 0;
            _columnInfos = new List<DataTableColumnInfo<T>>();

            //var t = typeof(T);
            var pos = 0;
            var sb = new StringBuilder();
            var md = (MetadataTypeAttribute)typeof(T).GetCustomAttributes(typeof(MetadataTypeAttribute), true).Single();
            var mdt = md.MetadataClassType;
            var props = mdt.GetProperties().Where(x => x.GetCustomAttributes(typeof(JQDT), true).Length > 0);
           // var props2 = mdt.GetProperties().Where(x => x.GetCustomAttributes(typeof(JQDTChildAttribute), true).Length > 0);




            int currentLimit = 0;
            foreach (var info in props)
            {

               // var is2 = props2.Select(x => x.Name).ToList().Contains(info.Name);

                var position = int.MaxValue;
                var hidden = false;
                var altName = "";

                var attrs = info.GetCustomAttributes(true);
                foreach (var jqdt in attrs.OfType<JQDT>())
                {
                    position = jqdt.Order;
                    hidden = jqdt.Hidden;
                    
                }

                foreach (var jqdt in attrs.OfType<JQDTChildAttribute>())
                {
                    altName = jqdt.EFName;

                }


                if (limit == null || currentLimit < limit)
                {

                    var abbr = GetAbbr(info);

                    if (_abbreviationCount.ContainsKey(abbr))
                    {
                        ++_abbreviationCount[abbr];
                    }
                    else
                    {
                        _abbreviationCount.Add(abbr, 0);
                    }

                    var count = _abbreviationCount[abbr] > 0 ? _abbreviationCount[abbr].ToString() : "";

                        pos = pos + 1;

                    var efname =  altName != "" ? altName : info.Name;
                    var newColumn = new DataTableColumnInfo<T>()
                                        {
                                            LongName = info.Name,
                                            Position = (position != int.MaxValue) ? position : pos,
                                            ShortName = abbr + count,
                                            FormattedName = info.Name.ToSpaced().Replace(" I D"," ID"),
                                            Hidden = hidden,
                                            IsString = info.PropertyType == typeof(string),
                                            EFName = efname
                                        };

                    _columnInfos.Add(newColumn);
                    sb.AppendLine(" " + newColumn.Position + " " + newColumn.LongName + " " + newColumn.ShortName + " " +
                                  newColumn.AltName + "|");
                    currentLimit++;
                }
            }
        }

        private string GetAbbr(PropertyInfo info)
        {

            var abbr = rgx.Replace(info.Name, "").ToLower();

            if (abbr == "")
                abbr = info.Name.Substring(0, 1).ToLower();

            return abbr;
        }


        private int _limit;
        public int Limit
        {
            get { return _limit; }
            set { _limit = value; }
        }


        private Dictionary<string, int> _abbreviationCount;

        public Dictionary<string, int> AbbreviationCount
        {
            get { return _abbreviationCount; }
            set { _abbreviationCount = value; }
        }

        private IList<DataTableColumnInfo<T>> _columnInfos;
        public IList<DataTableColumnInfo<T>> ColumnInfos
        {
            get { return 
                Limit > 0 ?
                _columnInfos.Take(Limit).OrderBy(x => x.Position).ToList()
                : _columnInfos.OrderBy(x => x.Position).ToList(); 
                
            }
            set { _columnInfos = value; }
        }


        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
