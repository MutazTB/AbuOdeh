namespace AbuOdeh_Electromechanical.Repository.Entities
{
    public class Product : Entity
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public double Price { get; set; }
        public string ImageName { get; set; }
        public Guid CategoryId { get; set; }
    }
}
