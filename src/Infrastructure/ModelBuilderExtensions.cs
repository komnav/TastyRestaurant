// using System.Text;
// using Microsoft.EntityFrameworkCore;
//
// namespace Restaurant.Tests.Integration;
//
// public static class ModelBuilderExtensions
// {
//     public static ModelBuilder UseSnakeCaseNamingConvention(this ModelBuilder modelBuilder)
//     {
//         foreach (var entity in modelBuilder.Model.GetEntityTypes())
//         {
//             entity.SetTableName(ToSnakeCase(entity.GetTableName()!));
//
//             foreach (var property in entity.GetProperties())
//             {
//                 property.SetColumnName(ToSnakeCase(property.GetColumnName()!));
//             }
//         }
//
//         return modelBuilder;
//     }
//
//     private static string ToSnakeCase(string input)
//     {
//         if (string.IsNullOrEmpty(input))
//             return input;
//
//         var sb = new StringBuilder();
//         for (int i = 0; i < input.Length; i++)
//         {
//             if (i > 0 && char.IsUpper(input[i]))
//                 sb.Append('_');
//
//             sb.Append(char.ToLower(input[i]));
//         }
//
//         return sb.ToString();
//     }
// }