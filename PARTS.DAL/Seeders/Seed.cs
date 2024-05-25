using Microsoft.Extensions.DependencyInjection;
using PARTS.DAL.Entities;
using PARTS.DAL.Entities.Vehicle;
using PARTS.DAL.Entities.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PARTS.DAL.Data;

namespace PARTS.DAL.Seeders
{
    public static class Seed
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<PartsDBContext>();

            if (context.Vehicles.Any() || context.Parts.Any() || context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            // Створення категорій
            var categories = new List<Category>
        {
            new Category
            {
                Id = Guid.NewGuid(),
                Title = "Engine Parts",
                Description = "Parts related to engine performance and maintenance",
                Parts = new List<Part>()
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Title = "Body Parts",
                Description = "Parts related to vehicle body and structure",
                Parts = new List<Part>()
            }
        };

            // Створення деталей
            var parts = new List<Part>
        {
            new Part
            {
                Id = Guid.NewGuid(),
                PartNumber = "ENG123",
                ManufacturerNumber = "MFG123",
                Description = "Engine Oil Filter",
                PartName = "Oil Filter",
                IsUniversal = true,
                PriceRegular = 25,
                PartTitle = "High Performance Oil Filter",
                PartAttributes = "Universal Fit",
                IsMadeToOrder = false,
                FitNotes = "Fits most cars",
                Count = 100,
                CategoryId = categories[0].Id,
                Orders = new List<Order>()
            },
            new Part
            {
                Id = Guid.NewGuid(),
                PartNumber = "BDY456",
                ManufacturerNumber = "MFG456",
                Description = "Car Door",
                PartName = "Front Left Door",
                IsUniversal = false,
                PriceRegular = 200,
                PartTitle = "Sedan Front Left Door",
                PartAttributes = "Color: Black",
                IsMadeToOrder = false,
                FitNotes = "Fits only sedan models",
                Count = 10,
                CategoryId = categories[1].Id,
                Orders = new List<Order>()
            }
        };

            // Додавання частин до категорій
            categories[0].Parts.Add(parts[0]);
            categories[1].Parts.Add(parts[1]);

            // Створення транспортних засобів
            var vehicles = new List<Vehicle>
        {
            new Vehicle
            {
                Id = Guid.Parse("b5a0c2e2-3d4f-4dd3-b499-98d7e16a5360"),
                FullModelName = "Toyota Camry",
                VIN = "12345ABCDE67890",
                Year = new DateTime(2020, 1, 1),
                URL = "https://toyota.com/camry",
                Parts = new List<Part> { parts[0] }
            },
            new Vehicle
            {
                Id = Guid.Parse("88c2a122-9e71-4a7a-a52d-9f82a6610d87"),
                FullModelName = "Honda Accord",
                VIN = "09876ZYXWV54321",
                Year = new DateTime(2019, 1, 1),
                URL = "https://honda.com/accord",
                Parts = new List<Part> { parts[1] }
            }
        };

            context.Categories.AddRange(categories);
            context.Parts.AddRange(parts);
            context.Vehicles.AddRange(vehicles);

            await context.SaveChangesAsync();
        }
    }
}