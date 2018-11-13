using Joinup.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Joinup.Helpers
{
    public static class TextFormatterHelper
    {
        public static string GetCategoryName(int pCategoryTypeId)
        {
            switch (pCategoryTypeId)
            {
                case PlanType.RESTAURANT:
                    return "Comida y bebida";
                case PlanType.SHOPPING:
                    return "Tiendas y compras";
                case PlanType.LANGUAGE:
                    return "Intercambio de idiomas";
                case PlanType.GOOUTFORDRINK:
                    return "Bares y ocio nocturno";
                case PlanType.OTHER:
                    return "Ocio y diversion";
                case PlanType.SPECTACLE:
                    return "Cine y espectaculos";
                case PlanType.TAKESOMETHING:
                    return "Tomar algo";
                case PlanType.TRAVEL:
                    return "Viajes y excursiones";
                case PlanType.SPORT:
                    return "Ejercicio físico y deporte";
                default:
                    return "Categoria no definida";



            }
        }
    }
}
