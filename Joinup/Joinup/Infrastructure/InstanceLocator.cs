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

        public InstanceLocator()
        {
            Main = new MainViewModel();
            Login = new LoginViewModel();
        }
    }
}
