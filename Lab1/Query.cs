using System;
using System.Collections.Generic;
using System.Linq;
using Lab1.Containers;

namespace Lab1
{
    internal class Query
    {
        public readonly List<Product> products;

        public readonly List<Storage> storages;

        public readonly List<Manufacturer> manufacturers;

        public readonly List<ProductManufacturer> productManufacturers;

        public Query(Lists lists)
        {
            products = lists.Products;
            storages = lists.Storages;
            manufacturers = lists.Manufacturers;
            productManufacturers = lists.ProductManufacturers;
        }

        public void ExecuteQueries()
        {
            Console.WriteLine("\n1");
            foreach (var item in GetAllManufacturer())
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n2");
            foreach (var item in GetAllProductsSortedDescByCost())
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n3");
            foreach (var item in GetAllProductsSortedByName())
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n4");
            foreach (var item in GetManufacturersNameStartsWith("В"))
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n5");
            foreach (var item in GetProductsCostGreaterThan(30m))
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n6");
            Console.WriteLine("Загальна кількість товарів = {0}", GetTotalNumberOfProducts());

            Console.WriteLine("\n7");
            foreach (var item in GetProductsGroupStorage())
            {
                Console.WriteLine("Склад: {0}", item.Key);
                foreach (var product in item.Value)
                {
                    Console.WriteLine("\t{0} - {1} (штук)", product.Name, product.Quantity);
                }
            }

            Console.WriteLine("\n8");
            foreach (var item in GetProductManufacturer())
            {
                Console.WriteLine("{0} - {1}", item.Product.Name, item.Manufacturer.Name);
            }

            Console.WriteLine("\n9");
            foreach (var item in GetProductGroupManufacturer())
            {
                Console.WriteLine("{0}", item.Key);
                foreach (var product in item.Value)
                {
                    Console.WriteLine("\t{0}", product.Name);
                }
            }

            Console.WriteLine("\n10");
            Console.WriteLine("Усі назви:");
            foreach (var item in GetAllSortedNames())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n11");
            foreach (var item in GetFullProductInfo())
            {
                Console.Write("{0} на {1}. Виробники:", item.Product.Name, item.Storage.Name);
                foreach (var manufacturer in item.Manufacturers)
                {
                    Console.Write(" {0}", manufacturer.Name);
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n12");
            foreach (var item in GetStorageTotalProductsAndCost())
            {
                Console.WriteLine("{0}: {1} товарів, загальною вартістю {2}", item.Storage.Name, item.TotalProducts,
                    item.TotalCost);
            }
            Console.WriteLine("\n13");
            foreach (var item in GetMostExpensiveProductByManufacturer())
            {
                Console.WriteLine("{0}: \n\tТовар з найвищою вартістю: {1}, вартість {2}", item.Manufacturer,
                    item.Product.Name, item.Product.Cost);
            }
            Console.WriteLine("\n14");
            foreach (var item in GetProductWithMoreThanOneManufacturer())
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("\n15");
            foreach (var item in GetProductArrivalOnlyThisYear())
            {
                Console.WriteLine("{0}", item.ToString());
            }
        }

        //1
        public IEnumerable<Manufacturer> GetAllManufacturer()
        {
            var query = from p in manufacturers select p;
            return query;
        }
        //2
        public IEnumerable<Product> GetAllProductsSortedDescByCost()
        {
            var query = products.OrderByDescending(p => p.Cost);
            return query;
        }
        //3
        public IEnumerable<Product> GetAllProductsSortedByName()
        {
            var query = from p in products orderby p.Name select p;
            return query;
        }
        //4
        public IEnumerable<Manufacturer> GetManufacturersNameStartsWith(string letter)
        {
            var query = manufacturers.Where(p => p.Name.StartsWith(letter));
            return query;
        }
        //5
        public IEnumerable<Product> GetProductsCostGreaterThan(decimal cost)
        {
            var query = from p in products where p.Cost > cost select p;
            return query;
        }
        //6
        public int GetTotalNumberOfProducts()
        {
            var query = products.Sum(p => p.Quantity);
            return query;
        }
        //7
        public Dictionary<int, List<Product>> GetProductsGroupStorage()
        {
            var query = from p in products
                        group p by p.StorageId;

            return query.ToDictionary(x => x.Key, x => x.ToList());
        }
        //8
        public IEnumerable<ProductManufacturerContainer> GetProductManufacturer()
        {
            var query = from p in products
                        join pm in productManufacturers on p.ProductId equals pm.ProductId
                        join m in manufacturers on pm.ManufacturerId equals m.ManufacturerId
                        select new ProductManufacturerContainer
                        {
                            Product = p,
                            Manufacturer = m
                        };

            return query;
        }
        //9
        public Dictionary<Manufacturer, List<Product>> GetProductGroupManufacturer()
        {
            var query = products
                .Join(productManufacturers, p => p.ProductId, pm => pm.ProductId,
                    (p, pm) => new { p, pm })
                .Join(manufacturers, t => t.pm.ManufacturerId, m => m.ManufacturerId,
                    (t, m) => new { t.p, m })
                .GroupBy(x => x.m, x => x.p);

            return query.ToDictionary(x => x.Key, x => x.ToList());
        }
        //10
        public IEnumerable<string> GetAllSortedNames()
        {
            var query = products.Select(p => p.Name)
                .Concat(manufacturers.Select(p => p.Name))
                .Concat(storages.Select(s => s.Name))
                .OrderBy(x => x);

            return query;
        }
        //11
        public IEnumerable<ProductInfoContainer> GetFullProductInfo()
        {
            var query = products
                        .Join(productManufacturers, product => product.ProductId, link => link.ProductId,
                            (product, link) => new { product, link })
                        .Join(manufacturers, t => t.link.ManufacturerId, manufacturer => manufacturer.ManufacturerId,
                            (t, manufacturer) => new { t.product, manufacturer })
                        .Join(storages, t => t.product.StorageId, storage => storage.StorageId,
                            (t, storage) => new { t, storage })
                        .GroupBy(x => x.t.product)
                        .Select(x => new ProductInfoContainer
                        {
                            Product = x.Key,
                            Manufacturers = x.Select(m => m.t.manufacturer).ToList(),
                            Storage = x.Select(s => s.storage).FirstOrDefault()
                        });

            return query;
        }
        //12
        public IEnumerable<StorageContainer> GetStorageTotalProductsAndCost()
        {
            var query = from p in products
                        group p by p.StorageId into partition
                        select new StorageContainer
                        {
                            Storage = storages.FirstOrDefault(s => s.StorageId == partition.Key),
                            TotalProducts = partition.Sum(x => x.Quantity),
                            TotalCost = partition.Sum(x => x.Cost * x.Quantity)
                        };

            return query;
        }
        //13
        public IEnumerable<ProductManufacturerContainer> GetMostExpensiveProductByManufacturer()
        {
            var query = from p in products
                        join pm in productManufacturers on p.ProductId equals pm.ProductId
                        group p by pm.ManufacturerId into partition
                        select new ProductManufacturerContainer
                        {
                            Manufacturer = manufacturers.FirstOrDefault(m => m.ManufacturerId == partition.Key),
                            Product = partition.OrderBy(x => x.Cost).LastOrDefault()
                        };

            return query;
        }
        //14
        public IEnumerable<Product> GetProductWithMoreThanOneManufacturer()
        {
            var query = from pm in productManufacturers
                        group pm by pm.ProductId into partition
                        where partition.Count() > 1
                        from p in products
                        where p.ProductId == partition.Key
                        select p;

            return query;
        }
        //15
        public IEnumerable<Product> GetProductArrivalOnlyThisYear()
        {
            var query = from p in products
                        where p.DatesArrival.All(d => d.Year == DateTime.Now.Year)
                        select p;

            return query;
        }
    }
}
