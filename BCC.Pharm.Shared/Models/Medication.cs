namespace BCC.Pharm.Shared.Models
{
    public class Medication
    {
        public int Id { get; set; }
        public string ActiveSubstance { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}