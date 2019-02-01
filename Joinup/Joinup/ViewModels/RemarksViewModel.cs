using Joinup.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.ViewModels
{
    public class RemarksViewModel:BaseViewModel
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
                RaisePropertyChanged("Plan");
            }
        }
        #endregion
        #region Constructors
        public RemarksViewModel()
        {

        }
        public override Task InitializeAsync(object navigationData)
        {
            var parameter = (Plan)navigationData;

            Plan = parameter;

            return base.InitializeAsync(navigationData);
        }
        #endregion
        #region Commands
        #endregion
        #region Methods
        #endregion
    }
}
