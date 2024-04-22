using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsSystemAK.Models
{
    public partial class Item : ObservableObject
    {
        [ObservableProperty]
        public string id;

        [ObservableProperty]
        public string collectionId;
        
        [ObservableProperty]
        public string name;
        
        [ObservableProperty]
        public string description;

        [ObservableProperty]
        public double price;
       
        [ObservableProperty]
        public string status;

        [ObservableProperty]
        public int rating;

        [ObservableProperty]
        public string imagePath;


    }
}
