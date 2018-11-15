using Joinup.Common.Models;
using Joinup.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.ViewModels
{
    public class PlanViewModel:BaseViewModel
    {
        #region Attributes
        private Plan plan;
        ObservableCollection<Plan> pins = new ObservableCollection<Plan>();
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
        public ObservableCollection<Plan> Pins
        {
            get { return pins; }
            set
            {
                pins = value;
                RaisePropertyChanged( "Pins" );
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
                if ( plan.Latitude != Double.NaN && plan.Longitude != Double.NaN )
                {
                    Pins = new ObservableCollection<Plan>();
                    Pins.Add( plan );
                }
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
