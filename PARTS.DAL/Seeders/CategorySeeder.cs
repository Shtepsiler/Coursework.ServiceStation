using Bogus;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PARTS.DAL.Entities.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.DAL.Seeders
{
    public class CategorySeeder : ISeeder<Category>
    {
        public void Seed(EntityTypeBuilder<Category> builder)
        {
            var categoryFaker = new Faker<Category>()
                .RuleFor(p => p.Id, f => f.Random.Guid())
                .RuleFor(c => c.Title, f => f.Commerce.Department())
                .RuleFor(c => c.Description, f => f.Lorem.Sentence());

            builder.HasData(categoryFaker.Generate(10));
        }
    }
}
