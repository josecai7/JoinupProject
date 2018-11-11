using Joinup.Common.Models;
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
        #endregion
        #region Constructors
        public PlanViewModel()
        {

        }
        public override Task InitializeAsync(object navigationData)
        {
            var parameter = navigationData as Plan;

            if (parameter != null)
            {
                Plan = parameter;
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
