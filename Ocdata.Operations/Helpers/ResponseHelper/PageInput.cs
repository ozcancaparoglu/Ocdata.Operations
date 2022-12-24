namespace Ocdata.Operations.Helpers.ResponseHelper
{
    public class PagerInput
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalItems { get; private set; }

        public PagerInput(int pageIndex = 1, int pageSize = 100)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public void SetTotalItems(int totalItems)
        {
            TotalItems = totalItems;
        }
    }
}
