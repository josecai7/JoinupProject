using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.DatabaseModels
{
    public class Language
    {
        [Key]
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public IList<PlanLanguage> PlanLanguages { get; set; }
    }
}
