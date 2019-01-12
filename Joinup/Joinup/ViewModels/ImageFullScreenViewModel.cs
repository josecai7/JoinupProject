using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.ViewModels
{
    public class ImageFullScreenViewModel:BaseViewModel
    {
        #region Attributes
        private List<Path> paths = new List<Path>();
        #endregion
        #region Properties
        public List<Path> Paths
        {
            get
            {
                return paths;
            }
        }
        #endregion
        #region Constructors
        public ImageFullScreenViewModel()
        {
            
        }

        public override Task InitializeAsync(object navigationData)
        {
            List<string> stringPaths = navigationData as List<string>;

            foreach(string str in stringPaths)
            {
                paths.Add(new Path { ComepletePath = str });
            }

            RaisePropertyChanged("Paths");

            return base.InitializeAsync(navigationData);
        }
        #endregion
        #region Commands
        #endregion
        #region Methods
        #endregion
    }
}
public class Path
{
    public string ComepletePath { get; set; }
}
