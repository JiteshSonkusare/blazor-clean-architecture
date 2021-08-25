using System;
using System.Linq;

namespace Client.Infrastructure.Routes
{
    public class ProductEndpoints
    {
        public static string GetAllPaged(int pageNumber, int pageSize, string searchString, string[] orderBy)
        {
            var url = $"api/v1/products?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
            if (orderBy?.Any() == true)
            {
                foreach (var orderByPart in orderBy)
                {
                    url += $"{orderByPart},";
                }
                url = url[..^1];
            }
            return url;
        }

        public static string GetById(Guid id)
        {
            return $"api/v1/products/{id}";
        }

        public static string Upsert = "api/v1/products";

        public static string Delete(Guid id)
        {
            return $"api/v1/products/{id}";
        }
    }
}
