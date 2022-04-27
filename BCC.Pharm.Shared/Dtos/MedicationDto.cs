namespace BCC.Pharm.Shared.Dtos
{
    public class MedicationDto
    {
        public int Id { get; set; }
        public string ActiveSubstance { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}