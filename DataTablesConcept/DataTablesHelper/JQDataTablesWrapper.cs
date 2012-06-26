using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DataTablesHelper
{
    public class JQDataTablesWrapper<T> : DataTableBase<T> where T : new()
    {
        public bool PreProcessed { get; private set; }
        public JQDataTablesWrapper(IEnumerable<T> col, int? limit = null, int? totalCount = null, int? filteredCount = null) : base(new T(), limit)

        {
            Collection = col;

            if (totalCount.HasValue)
            {
                PreProcessed = true;
                TotalCount = totalCount.Value;
                FilteredCount =  filteredCount.HasValue ? filteredCount.Value : totalCount.Value;
            }
            else
            {
                PreProcessed = false;
                TotalCount = Collection.Count();
                FilteredCount = TotalCount;
            }
        }

        public IEnumerable<T> Collection { get; set; }
        public Array GetInitList
        {
            get
            {
                return ColumnInfos.Select(x => new
                {
                    mDataProp = x.ShortName,
                    bVisible = !x.Hidden
                }).ToArray();



            }
        }

        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }

        public string DataTableInitJson(DataTableRequest<T> request)
        {

            


                var p2 = new List<object>();
                foreach (var pro in Collection)
                {
                    var p = ColumnInfos.ToDictionary(op => op.ShortName, op => GetPropValue(pro, op.LongName));
                    p2.Add(p);
                }

                var wrapper = new Dictionary<string, object>
                              {
                                  {"sEcho", request.sEcho + 1},
                                  {"iTotalRecords", TotalCount},
                                  {"iTotalDisplayRecords", FilteredCount},
                                  {"aaData", 
                                      
                                      PreProcessed ? p2 :
                                      
                                      p2.Skip(request.iDisplayStart).Take(request.iDisplayLength)
                                      
                                      }
                              };




                return JsonConvert.SerializeObject(
                    wrapper
                    , new IsoDateTimeConverter());

            
        }


    }


    public static class MyExtensions
    {
        public static string ToJson(this object o)
        {
            return JsonConvert.SerializeObject(o, new IsoDateTimeConverter());
        }


        public static string ToSpaced(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

    }



}


