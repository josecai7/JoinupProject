﻿using Joinup.ViewModels;
using Joinup.ViewModels.Base;
using Joinup.Views;
using Joinup.Views.Base;
using Joinup.Views.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Joinup.Navigation
{
    public class NavigationService : INavigationService
    {
        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(PlansViewModel), typeof(PlansPage) },
            { typeof(NewPlanStep1ViewModel), typeof(NewPlanStep1Page) },
            { typeof(NewPlanStep2ViewModel), typeof(NewPlanStep2Page) },
            { typeof(NewPlanStep3ViewModel), typeof(NewPlanStep3Page) },
            { typeof(NewPlanStep4ViewModel), typeof(NewPlanStep4Page) },
            { typeof(NewPlanStep5ViewModel), typeof(NewPlanStep5Page) },
        };


        protected readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication
        {
            get
            {
                return Application.Current;
            }
        }

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public Task InitializeAsync()
        {
            return NavigateToAsync<MainViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return NavigateToAsync( typeof( TViewModel ), null );
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return NavigateToAsync( typeof( TViewModel ), parameter );
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return NavigateToAsync( viewModelType, null );
        }

        protected virtual async Task NavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage( viewModelType, parameter );

            if ( page is MainView )
            {
                CurrentApplication.MainPage = new NavigationPage(page);
            }
            else if ( CurrentApplication.MainPage is MainView )
            {
                var mainPage = CurrentApplication.MainPage as MainView;
                var navigationPage = mainPage.CurrentPage as NavigationPage;
 
                if ( navigationPage != null )
                {
                    await navigationPage.PushAsync( page );
                }
                else
                {
                    navigationPage = new NavigationPage( page );
                    CurrentApplication.MainPage = navigationPage;
                }

            }
            else
            {
                var navigationPage = CurrentApplication.MainPage as NavigationPage;

                if ( navigationPage != null )
                {
                    await navigationPage.PushAsync( page );
                }
                else
                {
                    CurrentApplication.MainPage = new NavigationPage( page );
                }
            }
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if ( !_mappings.ContainsKey( viewModelType ) )
            {
                throw new KeyNotFoundException( $"No map for ${viewModelType} was found on navigation mappings" );
            }

            return _mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
                Type pageType = GetPageTypeForViewModel(viewModelType);

                if (pageType == null)
                {
                    throw new Exception($"Mapping type for {viewModelType} is not a page");
                }

                Page page = Activator.CreateInstance(pageType) as Page;
                BaseViewModel viewModel = ViewModelLocator.Instance.Resolve(viewModelType) as BaseViewModel;
                page.BindingContext = viewModel;

                page.Appearing += async (object sender, EventArgs e) =>
                {
                    await viewModel.InitializeAsync(parameter);
                };
                page.Disappearing += async (object sender, EventArgs e) =>
                {
                    await viewModel.OnDissapearing();
                };
                return page;
            
        }
        private void CreatePageViewModelMappings()
        {
            _mappings.Add( typeof( PlansViewModel ), typeof( PlansPage ) );
            _mappings.Add( typeof( NewPlanStep1ViewModel ), typeof( NewPlanStep1Page ) );
            _mappings.Add( typeof( NewPlanStep2ViewModel ), typeof( NewPlanStep2Page ) );
            _mappings.Add( typeof( NewPlanStep3ViewModel ), typeof( NewPlanStep3Page ) );
            _mappings.Add( typeof( NewPlanStep4ViewModel ), typeof( NewPlanStep4Page ) );
            _mappings.Add( typeof( NewPlanStep5ViewModel ), typeof( NewPlanStep5Page ) );
            _mappings.Add( typeof( MainViewModel ), typeof( MainView ) );
        }

        public async Task NavigateBackAsync()
        {
            if ( CurrentApplication.MainPage != null )
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }
    }
}