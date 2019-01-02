using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.SelectablesModels
{
    public class FOODTYPE
    {
        public const int FASTFOOD = 0;
        public const int TAPASBAR = 1;
        public const int RESTAURANT = 2;
        public const int ETHNIC = 3;
        public const int ANYONE = 4;

        public static List<Category> GetAllFoodTypes()
        {
            List<Category> foodTypes = new List<Category>();
            foodTypes.Add(new Category { Id = ANYONE, Name = "Cualquiera" });
            foodTypes.Add(new Category { Id=FASTFOOD,Name="Comida rápida"});
            foodTypes.Add(new Category { Id = TAPASBAR, Name = "Bar de tapas" });
            foodTypes.Add(new Category { Id = RESTAURANT, Name = "Restaurante" });
            foodTypes.Add(new Category { Id = ETHNIC, Name = "Comida étnica" });
            return foodTypes;
        }
    }
}
