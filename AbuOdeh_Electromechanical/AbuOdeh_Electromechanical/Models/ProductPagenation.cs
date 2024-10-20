namespace AbuOdeh_Electromechanical.Models
{
    public class ProductPagenation
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public Guid? CategoryId { get; set; }
        public double? FromPrice { get; set; }
        public double? ToPrice { get; set; }

    }
}
