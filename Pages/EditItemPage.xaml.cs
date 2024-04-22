using CollectionsSystemAK.ViewModels;
namespace CollectionsSystemAK.Pages;

public partial class EditItemPage : ContentPage
{
    public EditItemPage()
    {
        InitializeComponent();
        BindingContext = ItemsViewModel.Instance;
    }
}
