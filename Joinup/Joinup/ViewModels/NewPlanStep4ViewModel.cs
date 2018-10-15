using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Utils;
using Joinup.Views;
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
        TimeSpan _selectedTimeFrom;
        TimeSpan _selectedTimeTo;
        DateTime _minDate;
        string _dateFrom;
        string _timeFrom;
        string _dateTo;
        string _timeTo;
        #endregion
        #region Properties
        public SelectionRange SelectedRange
        {
            get { return _selectrange; }
            set
            {
                SetValue(ref _selectrange, value);
                DateFrom = _selectrange.StartDate.ToString( "dd-MM" );
                DateTo = _selectrange.EndDate.ToString( "dd-MM" );
            }
        }
        public TimeSpan SelectedTimeFrom
        {
            get { return _selectedTimeFrom; }
            set
            {
                SetValue( ref _selectedTimeFrom, value );
                TimeFrom = _selectedTimeFrom.Hours.ToString()+": "+_selectedTimeFrom.Minutes.ToString();
            }
        }
        public TimeSpan SelectedTimeTo
        {
            get { return _selectedTimeTo; }
            set
            {
                SetValue( ref _selectedTimeTo, value );
                TimeTo = _selectedTimeTo.Hours.ToString() + ": " + _selectedTimeTo.Minutes.ToString();
            }
        }
        public DateTime MinDate
        {
            get { return _minDate; }
            set { SetValue(ref _minDate, value); }
        }
        public string DateFrom
        {
            get { return string.IsNullOrEmpty(_dateFrom)?"-":_dateFrom; }
            set { SetValue( ref _dateFrom, value ); }
        }
        public string TimeFrom
        {
            get { return string.IsNullOrEmpty( _timeFrom ) ? "-" : _timeFrom; }
            set { SetValue( ref _timeFrom, value ); }
        }
        public string DateTo
        {
            get { return string.IsNullOrEmpty( _dateTo ) ? "-" : _dateTo; }
            set { SetValue( ref _dateTo, value ); }
        }
        public string TimeTo
        {
            get { return string.IsNullOrEmpty( _timeTo ) ? "-" : _timeTo; }
            set { SetValue( ref _timeTo, value ); }
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
        private async void NextStepAsync()
        {
            if ( SelectedRange==null || SelectedRange.StartDate == DateTime.MinValue || SelectedRange.EndDate == DateTime.MinValue )
            {
                ToastNotificationUtils.ShowToastNotifications( "Debe especificar la fecha", "add.png", ColorUtils.ErrorColor );
            }
            else if ( SelectedTimeFrom == TimeSpan.Zero || SelectedTimeTo == TimeSpan.Zero )
            {
                ToastNotificationUtils.ShowToastNotifications( "Debe especificar la hora", "add.png", ColorUtils.ErrorColor );
            }
            else
            {
                DateTime dateFrom = SelectedRange.StartDate + SelectedTimeFrom;
                DateTime dateTo = SelectedRange.EndDate + SelectedTimeTo;
                if ( dateTo < dateFrom )
                {
                    ToastNotificationUtils.ShowToastNotifications( "La fecha fin no puede ser superior a la hora inicio", "add.png", ColorUtils.ErrorColor );
                }
                else
                {
                    plan.PlanDate = dateFrom;
                    plan.EndPlanDate = dateTo;

                    MainViewModel.GetInstance().NewPlanStep5 = new NewPlanStep5ViewModel( plan );
                    await Application.Current.MainPage.Navigation.PushAsync( new NewPlanStep5Page() );
                }
            }
        }
        #endregion
    }
}
