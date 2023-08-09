using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using TestApp.Models;
using TestApp.ViewModels;

namespace TestApp.Views;

public partial class MainViewPage : ContentView
{
    private readonly DataViewModel dataView;

    public MainViewPage(DataViewModel dataView)
	{
		InitializeComponent();
        
        BindingContext = dataView;
        this.dataView = dataView;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPage(dataView));
    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        await Navigation.PushAsync(new DetailsPage(dataView));
    }

    private  void ContentView_Loaded(object sender, EventArgs e)
    {


        if (dataView.DataModels.Count == 0)
        {
            dataView.Msg = "No Data Added Yet";
        }
        else
        {
            dataView.Msg = string.Empty;
        }
        dataView.SelectedItem = null;
        
    }
    List<DataModel> newlist;
    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        newlist = new List<DataModel>();
        var list = dataView.AppDb.dataModels.ToList();
        if (!string.IsNullOrEmpty(dataView.SearchB))
        {
            await Task.Run(() =>
            {
                foreach (var l in list)
                {
                    if (l.Name.ToLower().Contains(dataView.SearchB))
                    {
                        newlist.Add(l);
                    }
                }
            });
            dataView.Msg = string.Empty;
            dataView.DataModels = newlist;
            if (dataView.DataModels.Count==0)
            {
                dataView.Msg = $"No Data Has {dataView.SearchB}";
            }
        }
        else
        {
            if (list.Count == 0)
            {
                dataView.Msg = "No Data Added Yet";
            }
            else
            {
                dataView.DataModels = list;
                dataView.Msg = string.Empty;
            }
            
        }
        

    }

    

}