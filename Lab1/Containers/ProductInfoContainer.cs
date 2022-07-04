namespace Lab1.Containers
{
    internal class ProductInfoContainer
    {
        public Product Product { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public Storage Storage { get; set; }
    }
}
