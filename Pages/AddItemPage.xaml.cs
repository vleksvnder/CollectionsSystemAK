using CollectionsSystemAK.ViewModels;

namespace CollectionsSystemAK.Pages;

public partial class AddItemPage : ContentPage
{
	public AddItemPage()
	{
		InitializeComponent();
        BindingContext = ItemsViewModel.Instance;
    }
}