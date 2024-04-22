using CollectionsSystemAK.Models;
using CollectionsSystemAK.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CollectionsSystemAK.ViewModels
{
    public partial class ItemsViewModel : ObservableObject
    {
        private Collection _selectedCollection;
        public Collection SelectedCollection
        {
            get => _selectedCollection;
            set
            {
                _selectedCollection = value;
                CollectionName = _selectedCollection.Name;
                CollectionId = _selectedCollection.Id;
                OnPropertyChanged(nameof(SelectedCollection));

            }
        }

        [ObservableProperty]
        public string collectionName;

        [ObservableProperty]
        public string collectionId;

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string description;

        [ObservableProperty]
        public string price;

        [ObservableProperty]
        public string status;

        [ObservableProperty]
        private string rating;

        [ObservableProperty]
        private string imagePath;


        private static ItemsViewModel _instance;
        public static ItemsViewModel Instance => _instance ??= new ItemsViewModel();

        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

        private string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "itemsdata.txt");
        public ItemsViewModel(Collection collection) 
        {
            SelectedCollection = collection;
            CollectionName = SelectedCollection.Name;
            CollectionId = SelectedCollection.Id;
        }
        public ItemsViewModel()
        {
            Debug.WriteLine(filePath);
            if (SelectedCollection != null)
            {
                CollectionName = SelectedCollection.Name;
                CollectionId = SelectedCollection.Id;
            }
        }

        [RelayCommand]
        private async Task AddImage()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Wybierz grafikę"
            });
            if (result != null)
            {
                var filePath = result.FullPath;
                ImagePath = filePath;
            }
        }

        [RelayCommand]
        public async void AddItem()
        {
            if (Items.Any(item =>
                    item.Name == Name &&
                    item.Description == Description &&
                    item.Status == Status
                    ))
            {
                await Application.Current.MainPage.DisplayAlert("Błąd!", "Element o takich samych danych już istnieje!", "OK");
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Błąd!", "Wymagana jest nazwa!", "OK");
            }
            else if (string.IsNullOrEmpty(Status))
            {
                await Application.Current.MainPage.DisplayAlert("Błąd!", "Wymagany jest status!", "OK");
            }
            else if (!Status.All(c => char.IsLower(c) || c == ' '))
            {
                await Application.Current.MainPage.DisplayAlert("Błąd!", "Status powinien zawierać tylko małe litery lub spacje!", "OK");
            }
            else
            {
                double priceParsed = double.TryParse(Price, out priceParsed) ? priceParsed : 0.0;
                int ratingParsed;
                if (!int.TryParse(Rating, out ratingParsed) || ratingParsed < 1 || ratingParsed > 10)
                {
                    await Application.Current.MainPage.DisplayAlert("Błąd!", "Ocena powinna być liczbą z zakresu od 1 do 10!", "OK");
                    return;
                }
                var newItem = new Item
                {
                    Id = NanoidDotNet.Nanoid.Generate(size: 5),
                    CollectionId = CollectionId,
                    Name = Name,
                    Description = Description ?? "Brak",
                    Price = priceParsed,
                    Status = Status ?? "Brak",
                    Rating = ratingParsed,
                    ImagePath = ImagePath
                };

                Items.Add(newItem);
                await SaveDataCollection();

                Name = string.Empty;
                Description = string.Empty;
                Price = string.Empty;
                Status = string.Empty;
                Rating = string.Empty;
                ImagePath = string.Empty;

                await Shell.Current.GoToAsync("..");
            }
        }


        [ObservableProperty]
        private Item selectedItem;

        [RelayCommand]
        public async Task EditItem()
        {
            await Shell.Current.GoToAsync($"{nameof(EditItemPage)}", true);
        }

        [RelayCommand]
        public async Task SaveItem()
        {
            if (Items.Any(item =>
                item.Id != SelectedItem.Id &&
                item.Name == SelectedItem.Name &&
                item.Description == SelectedItem.Description &&
                item.Price == SelectedItem.Price &&
                item.Status == SelectedItem.Status &&
                item.Rating == SelectedItem.Rating))
            {
                await Application.Current.MainPage.DisplayAlert("Błąd!", "Element o takich samych danych już istnieje!", "OK");
                return;
            }

            if (!string.IsNullOrEmpty(ImagePath))
            {
                SelectedItem.ImagePath = ImagePath;
            }
            else if (string.IsNullOrEmpty(SelectedItem.Name))
            {
                await Application.Current.MainPage.DisplayAlert("Błąd!", "Wymagana jest nazwa!", "OK");
                return;
            }
            else if (string.IsNullOrEmpty(SelectedItem.Status))
            {
                await Application.Current.MainPage.DisplayAlert("Błąd!", "Wymagany jest status!", "OK");
                return;
            }
            else if (!SelectedItem.Status.Replace(" ", "").All(char.IsLower))
            {
                await Application.Current.MainPage.DisplayAlert("Błąd!", "Status powinien zawierać tylko małe litery!", "OK");
                return;
            }
            else if (SelectedItem.Rating < 1 || SelectedItem.Rating > 10)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd!", "Ocena powinna być liczbą z zakresu od 1 do 10!", "OK");
                return;
            }

            await SaveDataCollection();
            await Shell.Current.GoToAsync("..");
        }



        [RelayCommand]
        public async Task GoToAddItem()
        {
            await Shell.Current.GoToAsync(nameof(AddItemPage));
        }

        [RelayCommand]
        public async Task SaveDataCollection()
        {
            var existingLines = File.Exists(filePath) ? await File.ReadAllLinesAsync(filePath) : [];
            var updatedLines = existingLines.Where(line => !line.StartsWith($"{CollectionId}::")).ToList();
            updatedLines.AddRange(Items.Select(item => $"{CollectionId}::{item.Id}::{item.Name}::{item.Description}::{item.Price}::{item.Status}::{item.Rating}::{item.ImagePath}"));

            await File.WriteAllLinesAsync(filePath, updatedLines);
        }

        [RelayCommand]
        public async Task LoadDataCollection()
        {
            Items.Clear();
            if (File.Exists(filePath))
            {
                var lines = await File.ReadAllLinesAsync(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split("::");
                    if (parts.Length == 8 && parts[0] == CollectionId)
                    {
                        var item = new Item
                        {
                            CollectionId = parts[0],
                            Id = parts[1],
                            Name = parts[2],
                            Description = parts[3],
                            Price = double.Parse(parts[4]),
                            Status = parts[5],
                            Rating = int.Parse(parts[6]),
                            ImagePath = parts[7]
                        };

                        if (item.Status.ToLower() == "sprzedane")
                        {
                            Items.Add(item);
                        }
                        else
                        {
                            Items.Insert(0, item);
                        }
                    }
                }
            }
        }


        [RelayCommand]
        public async Task DeleteItem()
        {
            if (SelectedItem != null)
            {
                Items.Remove(SelectedItem);
                await SaveDataCollection();
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        private async Task DisplayCollectionInfo()
        {
            if (SelectedCollection != null)
            {
                int soldItemsCount = Items.Count(item => item.Status.ToLower() == "sprzedane");
                int wantToSellItemsCount = Items.Count(item => item.Status.ToLower() == "chce sprzedać" || item.Status.ToLower() == "chcę sprzedać" || item.Status.ToLower() == "chce sprzedac");

                await Application.Current.MainPage.DisplayAlert(
                    "Informacje o kolekcji",
                    $"Nazwa: {SelectedCollection.Name}\n" +
                    $"Ilość wszystkich przedmiotów: {Items.Count}\n" +
                    $"Ilość przedmiotów sprzedanych: {soldItemsCount}\n" +
                    $"Ilość przedmiotów, które chcesz sprzedać: {wantToSellItemsCount}",
                    "OK");
            }
        }

    }
}
