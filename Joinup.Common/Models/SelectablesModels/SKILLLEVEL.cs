using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.SelectablesModels
{
    public class SKILLLEVEL
    {
        public const int UNDEFINED = 0;
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
    }
}
