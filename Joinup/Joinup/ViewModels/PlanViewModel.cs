using Joinup.Common.Models;
using Joinup.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.ViewModels
{
    public class PlanViewModel:BaseViewModel
    {
        #region Attributes
        private Plan plan;
        #endregion
        #region Properties
        public Plan Plan
        {
            get
            {
                return plan;
            }
            set
            {
                plan = value;
                RaisePropertyChanged();
            }
        }
        public string PlanType
        {
            get
            {
                return TextFormatterHelper.GetCategoryName(plan.PlanType);
            }
        }
        #endregion
        #region Constructors
        public PlanViewModel()
        {
            plan = new Plan();
        }
        public override Task InitializeAsync(object navigationData)
        {
            var parameter = navigationData as Plan;

            if (parameter != null)
            {
                Plan = parameter;
                RaisePropertyChanged("PlanType");
            }

            return base.InitializeAsync(navigationData);
        }
        #endregion
        #region Commands
        #endregion
        #region Methods
        #endregion
    }
}
