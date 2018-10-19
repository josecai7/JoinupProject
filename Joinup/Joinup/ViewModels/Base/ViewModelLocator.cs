﻿using Joinup.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Lifetime;

namespace Joinup.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _unityContainer;
        private static readonly ViewModelLocator _instance = new ViewModelLocator();

        public static ViewModelLocator Instance
        {
            get
            {
                return _instance;
            }
        }

        public ViewModelLocator()
        {
            _unityContainer = new UnityContainer();

            // ViewModels
            _unityContainer.RegisterType<PlansViewModel>();
            _unityContainer.RegisterType<NewPlanStep1ViewModel>();
            _unityContainer.RegisterType<NewPlanStep2ViewModel>();
            _unityContainer.RegisterType<NewPlanStep3ViewModel>();
            _unityContainer.RegisterType<NewPlanStep4ViewModel>();
            _unityContainer.RegisterType<NewPlanStep5ViewModel>();
            _unityContainer.RegisterType<MainViewModel>();

            //Services
            _unityContainer.RegisterType<INavigationService, NavigationService>();
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _unityContainer.Resolve( type );
        }

        public void Register<T>(T instance)
        {
            _unityContainer.RegisterInstance<T>( instance );
        }

        public void Register<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>();
        }

        public void RegisterSingleton<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>( new ContainerControlledLifetimeManager() );
        }
    }
}