using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Joinup.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        #region Attributes
        private bool _plansTab;
        private bool _tab2;
        #endregion
        #region Properties

        public bool PlansTab
        {
            get { return _plansTab; }
            set
            {
                _plansTab = value;
                RaisePropertyChanged();
            }
        }
        public bool Tab2
        {
            get { return _tab2; }
            set
            {
                _tab2 = value;
                RaisePropertyChanged();
            }
        }

        #endregion
        #region Constructors
        public ProfileViewModel()
        {
            SetPlansTab();
        }
        #endregion
        #region Commands
        public ICommand PlansCommand
        {
            get
            {
                return new RelayCommand(SetPlansTab);
            }
        }

        public ICommand Tab2Command
        {
            get
            {
                return new RelayCommand(SetTab2);
            }
        }
        #endregion
        #region Methods
        private void SetPlansTab()
        {
            PlansTab = true;
            Tab2 = false;
        }

        private void SetTab2()
        {
            PlansTab = false;
            Tab2 = true;
        }
        #endregion
    }
}