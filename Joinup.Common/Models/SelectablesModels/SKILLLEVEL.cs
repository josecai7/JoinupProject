using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.SelectablesModels
{
    public class SKILLLEVEL
    {
        public const int ANYONE = 1;
        public const int LOW = 2;
        public const int MEDIUM = 3;
        public const int HIGH = 4;
        public const int PROFESSIONAL = 5;

        public static List<Category> GetAllSkillLevel()
        {
            List<Category> foodTypes = new List<Category>();
            foodTypes.Add( new Category { Id = ANYONE, Name = "Cualquiera" } );
            foodTypes.Add( new Category { Id = LOW, Name = "Bajo" } );
            foodTypes.Add( new Category { Id = MEDIUM, Name = "Medio" } );
            foodTypes.Add( new Category { Id = HIGH, Name = "Alto" } );
            foodTypes.Add( new Category { Id = PROFESSIONAL, Name = "Profesional" } );
            return foodTypes;
        }

        public static string GetLevelTypeById(int pLevelTypeId)
        {
            switch (pLevelTypeId)
            {
                case ANYONE:
                    return "Cualquier nivel recomendado";
                case LOW:
                    return "Nivel bajo recomendado";
                case MEDIUM:
                    return "Nivel medio recomendado";
                case HIGH:
                    return "Nivel alto recomendado";
                case PROFESSIONAL:
                    return "Nivel muy alto recomendado";
                default:
                    return string.Empty;
            }

        }
    }
}
