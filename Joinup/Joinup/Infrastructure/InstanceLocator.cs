using Joinup.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Joinup.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }
        public LoginViewModel Login { get; set; }

        public PlansViewModel Plan { get; set; }

        public NewPlanViewModel NewPlan { get; set; }

        public NewPlanStep1ViewModel NewPlanStep1 { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
