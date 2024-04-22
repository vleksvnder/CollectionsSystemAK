using CollectionsSystemAK.Pages;

namespace CollectionsSystemAK
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CollectionItemsPage), typeof(CollectionItemsPage));
            Routing.RegisterRoute(nameof(AddItemPage), typeof(AddItemPage));
            Routing.RegisterRoute(nameof(EditItemPage), typeof(EditItemPage));
        }
    }
}
