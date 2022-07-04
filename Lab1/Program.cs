using System.Text;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Console.WriteLine("LINQ");

            Lists lists = new Lists();
            Query query = new Query(lists);
            query.ExecuteQueries();

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб вийти...");
            Console.ReadKey();
        }
    }
}
