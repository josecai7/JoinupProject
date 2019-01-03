using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.SelectablesModels
{
    public class SPORT
    {
        public const int FOOTBALL = 0;
        public const int BASKETBALL = 1;
        public const int TENIS = 2;
        public const int PADEL = 3;
        public const int HANDBALL = 4;
        public const int RUNNING = 5;
        public const int VOLEYBALL = 6;
        public const int RUGBY = 7;
        public const int OTHER = 8;

        public static List<Category> GetAllSports()
        {
            List<Category> foodTypes = new List<Category>();
            foodTypes.Add( new Category { Id = FOOTBALL, Name = "Fútbol" } );
            foodTypes.Add( new Category { Id = BASKETBALL, Name = "Baloncesto" } );
            foodTypes.Add( new Category { Id = TENIS, Name = "Tenis" } );
            foodTypes.Add( new Category { Id = PADEL, Name = "Padel" } );
            foodTypes.Add( new Category { Id = HANDBALL, Name = "Balonmano" } );
            foodTypes.Add( new Category { Id = RUNNING, Name = "Running" } );
            foodTypes.Add( new Category { Id = VOLEYBALL, Name = "Voleyball" } );
            foodTypes.Add( new Category { Id = RUGBY, Name = "Rugby" } );
            foodTypes.Add( new Category { Id = OTHER, Name = "Otro" } );
            return foodTypes;
        }
    }
}
