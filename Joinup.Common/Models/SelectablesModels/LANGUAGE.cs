using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.SelectablesModels
{
    public class LANGUAGE
    {
        public const int SPANISH = 0;
        public const int ENGLISH = 1;
        public const int FRENCH = 2;
        public const int GERMAN = 3;
        public const int PORTUGUESE = 4;
        public const int CHINESE = 5;

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
