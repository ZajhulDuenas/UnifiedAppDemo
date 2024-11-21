namespace Front.Models.Entitities
{
    public class EmployeeEntitie
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Rfc { get; set; } = string.Empty;
        public DateTime DateBirth { get; set; }
    }
}
