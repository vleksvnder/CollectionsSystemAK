using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsSystemAK.Models
{
    public partial class Collection : ObservableObject
    {
        [ObservableProperty]
        public string id;

        [ObservableProperty]
        public string name;

    }
}
