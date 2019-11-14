using Microsoft.EntityFrameworkCore;
using RetailBay.Common.Extensions;

namespace RetailBay.Infrastructure.EntityFramework
{
    /// <summary>
    /// ModelBuilder extension methods.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Uses the snake case naming convention.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static ModelBuilder UseSnakeCaseNamingConvention(this ModelBuilder builder, bool skipTableName = true)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                if (!skipTableName)
                    entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

                foreach (var property in entity.GetProperties())
                    property.Relational().ColumnName = property.Name.ToSnakeCase();

                foreach (var key in entity.GetKeys())
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();

                foreach (var key in entity.GetForeignKeys())
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();

                foreach (var index in entity.GetIndexes())
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
            }

            return builder;
        }
    }
}
