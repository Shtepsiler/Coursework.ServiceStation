using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Seeders;
namespace PARTS.DAL.Data.Configurations
{
    public class CategotyConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.Id);

            //   builder.HasMany(c => c.Parts).WithOne(p => p.Category);
            builder.HasOne(c => c.CategoryImage).WithOne(ci => ci.Category).HasForeignKey<CategoryImage>(ci => ci.CategoryId); // Ось цей рядок визначає зовнішній ключ
        /*    CategotySeeder brandSeeder = new CategotySeeder();
            brandSeeder.Seed(builder);*/

        }
    }
}
