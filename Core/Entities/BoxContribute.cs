namespace Core.Entities
{
    public class BoxContribute
    {
        public long BoxContributeId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public string ReferenceMonth { get; set; } = string.Empty;
        public string Observation { get; set; } = string.Empty;

        public long BoxId { get; set; }
        public Box? Box { get; set; }


    }
}
