namespace Lab1
{
    internal class Storage
    {
        public int StorageId { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public override string ToString()
        {
            return string.Format("Id: {0} Назва: {1}", StorageId, Name);
        }
    }
}
