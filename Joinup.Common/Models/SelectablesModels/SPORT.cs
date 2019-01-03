using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.SelectablesModels
{
    public class SPORT
    {
        public const int UNDEFINED = 0;
        public const int FOOTBALL = 1;
        public const int INDOORFOOTBALL = 2;
        public const int BASKETBALL = 3;
        public const int TENIS = 4;
        public const int PADEL = 5;
        public const int HANDBALL = 6;
        public const int RUNNING = 7;
        public const int VOLEYBALL = 8;
        public const int RUGBY = 9;
        public const int OTHER = 10;

        public static List<Category> GetAllSports()
        {
            List<Category> foodTypes = new List<Category>();
            foodTypes.Add( new Category { Id = FOOTBALL, Name = "Fútbol" } );
            foodTypes.Add( new Category { Id = INDOORFOOTBALL, Name = "Fútbol sala" } );
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
