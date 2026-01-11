namespace Core.Entities
{
    public class ReportCategory
    {
        public long ReportCategoryId { get; set; }
        public decimal Total { get; set; }
        public decimal Percent { get; set; }

        public long ReportId { get; set; }
        public Report? Report { get; set; }

        public long CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
