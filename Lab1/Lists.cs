using System;
using System.Collections.Generic;

namespace Lab1
{
    internal class Lists
    {
        private Product p1, p2, p3, p4, p5, p6, p7;

        public List<Product> Products { get; private set; }

        public List<Storage> Storages { get; private set; }

        public List<Manufacturer> Manufacturers { get; private set; }

        public List<ProductManufacturer> ProductManufacturers { get; private set; }

        public Lists()
        {
            InitializeProducts();
            InitializeLists();
        }

        private void InitializeProducts()
        {
            p1 = new Product
            {
                ProductId = 1,
                Name = "Книга",
                Cost = 34m,
                Quantity = 2,
                DatesArrival = new List<DateTime> { new DateTime(2021, 3, 2), new DateTime(2022, 1, 1) },
                StorageId = 1
            };
            p2 = new Product
            {
                ProductId = 2,
                Name = "Зошит",
                Cost = 14m,
                Quantity = 1,
                DatesArrival = new List<DateTime> { new DateTime(2021, 11, 4), new DateTime(2022, 1, 1) },
                StorageId = 1
            };
            p3 = new Product
            {
                ProductId = 3,
                Name = "Олівець",
                Cost = 30m,
                Quantity = 4,
                DatesArrival = new List<DateTime> { new DateTime(2021, 1, 25) },
                StorageId = 1
            };
            p4 = new Product
            {
                ProductId = 4,
                Name = "Ручка",
                Cost = 40m,
                Quantity = 2,
                DatesArrival = new List<DateTime> { new DateTime(2021, 4, 13) },
                StorageId = 2
            };
            p5 = new Product
            {
                ProductId = 5,
                Name = "Пенал",
                Cost = 15m,
                Quantity = 7,
                DatesArrival = new List<DateTime> { new DateTime(2021, 5, 14), new DateTime(2021, 5, 16) },
                StorageId = 2
            };
            p6 = new Product
            {
                ProductId = 6,
                Name = "Маркер",
                Cost = 33m,
                Quantity = 11,
                DatesArrival = new List<DateTime> { new DateTime(2022, 2, 2) },
                StorageId = 2
            };
            p7 = new Product
            {
                ProductId = 7,
                Name = "Лінійка",
                Cost = 22m,
                Quantity = 4,
                DatesArrival = new List<DateTime> { new DateTime(2022, 1, 9) },
                StorageId = 2
            };
        }

        private void InitializeLists()
        {
            Products = new List<Product> { p1, p2, p3, p4, p5, p6, p7 };

            Storages = new List<Storage>
            {
                new Storage { StorageId = 1, Name = "Склад 1", Products = new List<Product> { p1, p2, p3 } },
                new Storage { StorageId = 2, Name = "Склад 2", Products = new List<Product> { p4, p5, p6, p7 } }
            };

            Manufacturers = new List<Manufacturer>
            {
                new Manufacturer { ManufacturerId = 1, Name = "Виробник 1" },
                new Manufacturer { ManufacturerId = 2, Name = "Виробник 2" },
                new Manufacturer { ManufacturerId = 3, Name = "Виробник 3" }
            };

            ProductManufacturers = new List<ProductManufacturer>
            {
                new ProductManufacturer { ProductId = 1, ManufacturerId = 1 },
                new ProductManufacturer { ProductId = 1, ManufacturerId = 2 },
                new ProductManufacturer { ProductId = 2, ManufacturerId = 1 },
                new ProductManufacturer { ProductId = 2, ManufacturerId = 2 },
                new ProductManufacturer { ProductId = 3, ManufacturerId = 2 },
                new ProductManufacturer { ProductId = 4, ManufacturerId = 2 },
                new ProductManufacturer { ProductId = 7, ManufacturerId = 2 },
                new ProductManufacturer { ProductId = 5, ManufacturerId = 3 },
                new ProductManufacturer { ProductId = 6, ManufacturerId = 3 }
            };
        }
    }
}
