namespace Lab1
{
    internal class Manufacturer
    {
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return string.Format("Id: {0} Назва: {1}", ManufacturerId, Name);
        }
    }
}
