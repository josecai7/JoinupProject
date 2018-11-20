using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Utils;
using Joinup.ViewModels.Base;
using Joinup.Views;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        public double Progress
        {
            get
            {
                return plan.GetProgress( 4 );
            }
        }
        public SelectionRange SelectedRange
        {
            get
            {
                return _selectrange;
            }
            set
            {
                _selectrange = value;
                plan.PlanDate = new DateTime(_selectrange.StartDate.Year, _selectrange.StartDate.Month, _selectrange.StartDate.Day, plan.PlanDate.TimeOfDay.Hours, plan.PlanDate.TimeOfDay.Minutes, plan.PlanDate.TimeOfDay.Seconds);
                plan.EndPlanDate = new DateTime(_selectrange.EndDate.Year, _selectrange.EndDate.Month, _selectrange.EndDate.Day, plan.EndPlanDate.TimeOfDay.Hours, plan.EndPlanDate.TimeOfDay.Minutes, plan.EndPlanDate.TimeOfDay.Seconds);
                RaisePropertyChanged( "SelectedRange" );
                RaisePropertyChanged("DateFrom");
                RaisePropertyChanged("DateTo");               
            }
        }
        public string DateFrom
        {
            get
            {
                return DateTimeUtils.FormatDate(plan.PlanDate);
            }
            set
            {
                RaisePropertyChanged("DateFrom");
            }
        }
        public string DateTo
        {
            get
            {
                return DateTimeUtils.FormatDate(plan.EndPlanDate);
            }
            set
            {
                RaisePropertyChanged("DateTo");
            }
        }
        public TimeSpan SelectedTimeFrom
        {
            get
            {
                return plan.PlanDate.TimeOfDay;
            }
            set
            {
                plan.PlanDate = new DateTime(plan.PlanDate.Year, plan.PlanDate.Month,plan.PlanDate.Day,value.Hours,value.Minutes,value.Seconds);
                RaisePropertyChanged( "TimeFrom" );
            }
        }
        public TimeSpan SelectedTimeTo
        {
            get
            {
                return plan.EndPlanDate.TimeOfDay;
            }
            set
            {
                plan.EndPlanDate = new DateTime(plan.EndPlanDate.Year, plan.EndPlanDate.Month, plan.EndPlanDate.Day, value.Hours, value.Minutes, value.Seconds);
                RaisePropertyChanged("TimeTo");
            }
        }
        public DateTime MinDate
        {
            get
            {
                return _minDate;
            }
            set
            {
                _minDate = value;
                RaisePropertyChanged( "MinDate" );
            }
        }
        public string TimeFrom
        {
            get
            {
                return DateTimeUtils.FormatTime(plan.PlanDate);
            }
            set
            {
                RaisePropertyChanged("TimeFrom");
            }
        }
        public string TimeTo
        {
            get
            {
                return DateTimeUtils.FormatTime(plan.EndPlanDate);
            }
            set
            {
                RaisePropertyChanged("TimeTo");
            }
        }
        #endregion
        #region Constructors
        public NewPlanStep4ViewModel()
        {
            plan = new Plan();
            MinDate = DateTime.Today;
            MessagingCenter.Subscribe<NewPlanStep1ViewModel, Plan>(this, "UpdatePlan", (sender, arg) => {
                plan = arg;
            });
        }
        public override Task InitializeAsync(object navigationData)
        {
            plan = (Plan)navigationData;

            _selectrange = new SelectionRange(plan.PlanDate, plan.EndPlanDate);

            RaisePropertyChanged("SelectedRange");
            RaisePropertyChanged("TimeFrom");
            RaisePropertyChanged("TimeTo");

            return base.InitializeAsync(navigationData);
        }
        public override Task OnDissapearing()
        {
            MessagingCenter.Send(ViewModelLocator.Instance.Resolve<NewPlanStep1ViewModel>(), "UpdatePlan", plan);
            return base.OnDissapearing();
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
        public ICommand SelectedTimeFromCommand
        {
            get
            {
                return new RelayCommand<Object>( TimePickerFocus );
            }
        }
        #endregion
        #region Methods
        private void TimePickerFocus(Object pTimepicker)
        {
            var timepicker = (TimePicker) pTimepicker;
            timepicker.Focus();
        }
        private  void NextStepAsync()
        {
            if (plan.PlanDate == DateTime.MinValue || plan.EndPlanDate == DateTime.MinValue )
            {
                ToastNotificationUtils.ShowToastNotifications( "Debe especificar la fecha", "add.png", ColorUtils.ErrorColor );
            }
            else if (plan.PlanDate.TimeOfDay == TimeSpan.Zero || plan.EndPlanDate.TimeOfDay == TimeSpan.Zero )
            {
                ToastNotificationUtils.ShowToastNotifications( "Debe especificar la hora", "add.png", ColorUtils.ErrorColor );
            }
            else
            {
                if (plan.EndPlanDate < plan.PlanDate)
                {
                    ToastNotificationUtils.ShowToastNotifications( "La fecha fin no puede ser superior a la hora inicio", "add.png", ColorUtils.ErrorColor );
                }
                else
                {
                    NavigationService.NavigateToAsync<NewPlanStep5ViewModel>(plan);
                }
            }
        }
        #endregion
    }
}
