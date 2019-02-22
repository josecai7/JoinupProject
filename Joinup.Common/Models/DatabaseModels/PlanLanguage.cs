using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.DatabaseModels
{
    public class PlanLanguage
    {
        public int PlanId { get; set; }
        public Plan Plan { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
