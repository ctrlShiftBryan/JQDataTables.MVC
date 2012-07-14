namespace DataTablesHelper
{
    public class DataTableColumnInfo<T>
    {
        public string EFName { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public int Position { get; set; }
        public string AltName { get; set; }
        public string FormattedName { get; set; }
        public bool Hidden { get; set; }
        public bool IsString { get; set; }

    }
}
