using Ocdata.Operations.Enums;

namespace Ocdata.Operations.Helpers.ExpressionHelper
{
    public class FilterModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<FilterItem> Filters { get; set; }
    }

    public class FilterItem
    {
        public string Field { get; set; }
        public FilterOperatorEnum Operator { get; set; }
        public object Value { get; set; }
    }
}
