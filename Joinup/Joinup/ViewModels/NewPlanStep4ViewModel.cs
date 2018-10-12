using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Utils;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class NewPlanStep4ViewModel:BaseViewModel
    {
        #region Attributes
        private Plan plan;
        SelectionRange _selectrange;
        DateTime _minDate;
        #endregion
        #region Properties
        public SelectionRange SelectedRange
        {
            get { return _selectrange; }
            set { SetValue(ref _selectrange, value); }
        }
        public DateTime MinDate
        {
            get { return _minDate; }
            set { SetValue(ref _minDate, value); }
        }
        #endregion
        #region Constructors
        public NewPlanStep4ViewModel(Plan pPlan)
        {
            plan = pPlan;
            MinDate = DateTime.Today;
        }
        #endregion
        #region Commands
        public ICommand NextStepCommand
        {
            get
            {
                return new RelayCommand(NextStepAsync);
            }
        }
        #endregion
        #region Methods
        private async void NextStepAsync()
        {

            
        }
        #endregion
    }
}
