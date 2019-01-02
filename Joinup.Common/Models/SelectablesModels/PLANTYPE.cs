using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.SelectablesModels
{
    public class PLANTYPE
    {
        public const int UNDEFINED = 0;
        public const int FOODANDDRINK= 1;
        public const int SPECTACLES = 2;
        public const int SPORT = 3;
        public const int LANGUAGE = 4;
        public const int TRAVEL = 5;
        public const int SHOPPING = 6;
        public const int PARTY = 7;
        public const int OTHER = 8;

        public static List<Category> GetAllPlanTypes()
        {
            List<Category> foodTypes = new List<Category>();
            foodTypes.Add(new Category { Id = UNDEFINED, Name = "No definido" });
            foodTypes.Add(new Category { Id = FOODANDDRINK, Name = "Comida y bebida" });
            foodTypes.Add(new Category { Id = SPECTACLES, Name = "Conciertos y espectaculos" });
            foodTypes.Add(new Category { Id = SPORT, Name = "Deportes" });
            foodTypes.Add(new Category { Id = LANGUAGE, Name = "Idiomas" });
            foodTypes.Add(new Category { Id = TRAVEL, Name = "Viajes" });
            foodTypes.Add(new Category { Id = SHOPPING, Name = "Compras" });
            foodTypes.Add(new Category { Id = PARTY, Name = "Fiestas" });
            foodTypes.Add(new Category { Id = OTHER, Name = "Otras" });
            return foodTypes;
        }
    }
}
