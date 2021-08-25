using System;

namespace Client.Infrastructure.Routes
{
    public class BrandEndpoints
    {
        public static string GetAll = "api/v1/brands";

        public static string GetById(Guid id)
        {
            return $"api/v1/brands/{id}";
        }

        public static string Upsert = "api/v1/brands";

        public static string Delete(Guid id)
        {
            return $"api/v1/brands/{id}";
        }
    }
}
