using CollectionsSystemAK.Models;
using CollectionsSystemAK.ViewModels;
using System.Windows.Input;

namespace CollectionsSystemAK.Pages;

[QueryProperty(nameof(CollectionId), nameof(CollectionId))]
[QueryProperty(nameof(CollectionName), nameof(CollectionName))]
public partial class CollectionItemsPage : ContentPage
{
	private string collectionId, collectionName;
    public string CollectionId 
	{ 
		get => collectionId; 
		set 
		{ 
			collectionId = Uri.UnescapeDataString(value ?? string.Empty);
            UpdateViewModel();
		} 
	}
    public string CollectionName
    {
        get => collectionName;
        set
        {
            collectionName = Uri.UnescapeDataString(value ?? string.Empty);
            UpdateViewModel();
        }
    }

    public CollectionItemsPage()
	{
		InitializeComponent();
        BindingContext = ItemsViewModel.Instance;
    }

    private void UpdateViewModel()
    {
        if (!string.IsNullOrEmpty(CollectionId) && !string.IsNullOrEmpty(CollectionName))
        {
            Collection collection = new() { Id = CollectionId, Name = CollectionName };
            if (ItemsViewModel.Instance != null)
            {
                ItemsViewModel.Instance.SelectedCollection = collection;
                BindingContext = ItemsViewModel.Instance;
            }
        }
    }

    public async void OnItemSelected(object sender, SelectionChangedEventArgs e)
    {
        var viewModel = BindingContext as ItemsViewModel;
        if (e.CurrentSelection.FirstOrDefault() is Item selectedItem)
        {
            viewModel.SelectedItem = selectedItem;
            await viewModel.EditItem();
        }
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ItemsViewModel viewModel)
        {
            await viewModel.LoadDataCollection();
        }
    }

}