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
    public class BrandSeeder : ISeeder<Brand>
    {
        public void Seed(EntityTypeBuilder<Brand> builder)
        {
            var brandFaker = new Faker<Brand>()
                .RuleFor(p=>p.Id,f=>f.Random.Guid())
                .RuleFor(b => b.Title, f => f.Company.CompanyName())
                .RuleFor(b => b.Description, f => f.Lorem.Sentence());

            builder.HasData(brandFaker.Generate(10));
        }
    }
}
