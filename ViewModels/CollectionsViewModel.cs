using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CollectionsSystemAK.Models;
using CollectionsSystemAK.Pages;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CollectionsSystemAK.ViewModels
{
    public partial class CollectionsViewModel : ObservableObject
    {
        public ObservableCollection<Collection> Collections { get; set; } = new ObservableCollection<Collection>();
        private string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "collectionsdata.txt");

        public CollectionsViewModel()
        {
            Debug.WriteLine(filePath);
            LoadDataCollection();
        }


        [RelayCommand]
        public async Task AddCollection()
        {
            var name = await Application.Current.MainPage.DisplayPromptAsync("Dodaj kolekcję: ", "Podaj nazwe:");
            var id = NanoidDotNet.Nanoid.Generate(size: 5);

            if (!string.IsNullOrWhiteSpace(name))
            {
                Collections.Add(new Collection { Id = id, Name = name });
                await SaveDataCollection();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("BŁĄD!", "Uzupełnij pole!", "OK");
            }
        }

        [ObservableProperty]
        private Collection selectedCollection;

        [RelayCommand]
        public async Task DisplayItems()
        {
            if (SelectedCollection != null)
            {
                var collectionId = SelectedCollection.Id;
                var collectionName = Uri.EscapeDataString(SelectedCollection.Name);

                await Shell.Current.GoToAsync($"{nameof(CollectionItemsPage)}?CollectionId={collectionId}&CollectionName={collectionName}");
                SelectedCollection = null;
            }
        }

        /*[RelayCommand]
        public async Task DeleteCollection()
        {
            if (SelectedCollection != null)
            {
                Collections.Remove(SelectedCollection);
                await SaveDataCollection();
            }
        }*/

        [RelayCommand]
        public async Task SaveDataCollection()
        {
            var lines = Collections.Select(c => $"{c.Id}::{c.Name}").ToList();
            await File.WriteAllLinesAsync(filePath, lines);
        }

        [RelayCommand]
        public async Task LoadDataCollection()
        {
            if (File.Exists(filePath))
            {
                var lines = await File.ReadAllLinesAsync(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split("::");
                    if (parts.Length == 2)
                    {
                        Collections.Add(new Collection { Id = parts[0], Name = parts[1] });
                    }
                }
            }
        }
    }
}
