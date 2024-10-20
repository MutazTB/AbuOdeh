namespace AbuOdeh_Electromechanical.Repository
{
    public class Entity
    {
        public int Id { get; set; }
        public Guid ObjectKey { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;
        public DateTime CreateDate { get; private set; } = DateTime.Now;
        public DateTime? LastUpdatedDate { get; set; }
    }
}
