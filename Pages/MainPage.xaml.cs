using CollectionsSystemAK.ViewModels;
using System.Diagnostics;

namespace CollectionsSystemAK
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new CollectionsViewModel();
        }

    }

}
