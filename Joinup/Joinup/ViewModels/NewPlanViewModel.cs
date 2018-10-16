using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class NewPlanViewModel:BaseViewModel
    {
        #region Attributes
        private ApiService apiService;

        #endregion

        #region Properties

        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        
        #region Commands

        public ICommand SavePlanCommand
        {
            get
            {
                return new RelayCommand(SavePlan);
            }
        }

        private async void SavePlan()
        {
            Plan plan = new Plan();
            plan.Name = Name;
            plan.Description = Description;
            plan.EndPlanDate = DateTime.Now;
            plan.PlanDate = DateTime.Now;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPlansController"].ToString();
            var response = await apiService.Post<Plan>(url,prefix,controller,plan);

            var newplan = (Plan) response.Result;

            //var viewModel=PlansViewModel.GetInstance();
            //viewModel.PlanList.Add( newplan );


            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion

    }
}
