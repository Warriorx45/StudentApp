using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.ViewModels
{
    public partial class DataViewModel:ObservableObject
    {
        [ObservableProperty]
        string name;

        [ObservableProperty]
        string image;
        [ObservableProperty]
        int age;
        [ObservableProperty]
        AppDbContext appDb;

        [ObservableProperty]
        string msg;

        [ObservableProperty]
        List<DataModel> dataModels;

        [ObservableProperty]
        DataModel selectedItem;

        [ObservableProperty]
        string searchB;

        
        public DataViewModel()
        {
            
            Image = "john.png";
            Msg = "No Data Added Yet";
            DataModels = new List<DataModel>();
            AppDb = new AppDbContext();
            DataModels = AppDb.dataModels.ToList();
        }
        
    }
}
