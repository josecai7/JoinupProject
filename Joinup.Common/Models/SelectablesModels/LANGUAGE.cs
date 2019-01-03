using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.SelectablesModels
{
    public class LANGUAGE
    {
        public const int UNDEFINED = 0;
        public const int SPANISH = 1;
        public const int ENGLISH = 2;
        public const int FRENCH = 3;
        public const int GERMAN = 4;
        public const int PORTUGUESE = 5;
        public const int CHINESE = 6;

        public static List<Category> GetAllLanguages()
        {
            List<Category> foodTypes = new List<Category>();
            foodTypes.Add( new Category { Id = SPANISH, Name = "Español" } );
            foodTypes.Add( new Category { Id = ENGLISH, Name = "Inglés" } );
            foodTypes.Add( new Category { Id = FRENCH, Name = "Francés" } );
            foodTypes.Add( new Category { Id = GERMAN, Name = "Alemán" } );
            foodTypes.Add( new Category { Id = PORTUGUESE, Name = "Portugués" } );
            foodTypes.Add( new Category { Id = CHINESE, Name = "Chino" } );
            return foodTypes;
        }
    }
}
