using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using TestApp.Models;
using TestApp.ViewModels;

namespace TestApp.Views;

public partial class DetailsPage : ContentPage
{
    private readonly DataViewModel dataView;

    public DetailsPage(DataViewModel dataView)
	{
		InitializeComponent();
		BindingContext=dataView;
        this.dataView = dataView;
        var item = dataView.SelectedItem;
        dataView.Name = item.Name;
        dataView.Age = item.Age;
        dataView.Image = item.Image;
    }

    private async void Delete_btn_Clicked(object sender, EventArgs e)
    {
        var item = dataView.SelectedItem;

        var choose = await DisplayAlert("Choose", "Do You Want To Delete It", "Yes", "No");
        if (choose == true)
        {
            await Task.Run(() => dataView.AppDb.dataModels.Remove(item));
            await dataView.AppDb.SaveChangesAsync();
            await Task.Run(() => dataView.DataModels = dataView.AppDb.dataModels.ToList());
            dataView.Name = string.Empty;
            dataView.Age = 0;
            dataView.Image = "john.png";
            if (dataView.DataModels.Count == 0)
            {
                dataView.Msg = "No Data Added Yet";
            }
            await Toast.Make("Item Was Deleted", ToastDuration.Short, 14).Show();
            await Navigation.PopAsync();
        }
        
        
    }

    private async void Edit_btn_Clicked(object sender, EventArgs e)
    {
        DataModel data=new DataModel();
        try
        {
            await Task.Run(() =>
            {
                var list = dataView.AppDb.dataModels.ToList();
                foreach (var item in list)
                {
                    if (item == dataView.SelectedItem)
                    {
                        var ob = new DataModel
                        {
                            Id = dataView.SelectedItem.Id,
                            Name = dataView.Name,
                            Age = dataView.Age,
                            Image = dataView.Image,
                        };
                        data = ob;
                        
                        break;
                    }
                }
            });
            var choose = await DisplayAlert("Choose", "Do You Want To Edit It", "Yes", "No");
            if (choose == true)
            {
                await Task.Run(() =>
                {
                    dataView.AppDb.dataModels.Remove(dataView.SelectedItem);
                    dataView.AppDb.dataModels.AddAsync(data);
                    dataView.AppDb.SaveChangesAsync();
                    dataView.DataModels = dataView.AppDb.dataModels.ToList();
                });
                
                dataView.Name = string.Empty;
                dataView.Age = 0;
                dataView.Image = "john.png";
                await Toast.Make("Item Was Edited", ToastDuration.Short, 14).Show();
                await Navigation.PopAsync();
            }
            
        }
        catch(Exception ex)
        {
            await Toast.Make(ex.Message,ToastDuration.Short,14).Show();
        }
    }

    private async void AddImage_btn_Clicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Select An Image Please "
            });

            if (result != null)
            {
                var filePath = result.FullPath;
                dataView.Image = filePath;
            }

            await Toast.Make("Image Was Added", ToastDuration.Short, 14).Show();

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}