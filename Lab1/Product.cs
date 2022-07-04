using System.Text;

namespace Lab1
{
    internal class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public List<DateTime> DatesArrival { get; set; }
        public int StorageId { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("Id: {0} Назва: {1} Вартість: {2} Кількість = {3} Id складу: {4} Дати надходження:", 
                ProductId, Name, Cost, Quantity, StorageId));

            foreach (var item in DatesArrival)
            {
                sb.Append(" " + item.ToShortDateString());                
            }

            return sb.ToString();
        }
    }
}
