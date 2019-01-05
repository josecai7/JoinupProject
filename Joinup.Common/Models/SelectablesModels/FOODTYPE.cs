using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.SelectablesModels
{
    public class FOODTYPE
    {
        public const int ANYONE = 1;
        public const int FASTFOOD = 2;
        public const int TAPASBAR = 3;
        public const int RESTAURANT = 4;
        public const int ETHNIC = 5;
        

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
        public static string GetFoodTypeById(int pFoodTypeId)
        {
            switch(pFoodTypeId)
            {
                case FASTFOOD:
                    return "Comida rápida";
                case ANYONE:
                    return "Cualquier tipo de comida";
                case TAPASBAR:
                    return "Bar de tapas";
                case RESTAURANT:
                    return "Rastaurante";
                case ETHNIC:
                    return "Comida étnica";
                default:
                    return string.Empty;
            }

        }
    }
}
