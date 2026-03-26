namespace MormorDagnys.Entities
{
    public record Supplier
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<SupplierProduct> SupplierProducts { get; set; }
    }
}