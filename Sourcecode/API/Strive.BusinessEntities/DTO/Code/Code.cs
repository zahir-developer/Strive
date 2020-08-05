namespace Strive.BusinessEntities.Code
{
    public class Code
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int CodeId { get; set; }
        public string CodeValue { get; set; }
        public string CodeShortValue { get; set; }
        public int? Sortorder { get; set; }
    }
}
