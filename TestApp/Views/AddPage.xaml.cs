using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using TestApp.ViewModels;

namespace TestApp.Views;

public partial class AddPage : ContentPage
{
    private readonly DataViewModel dataView;

    public AddPage(DataViewModel dataView)
    {
        InitializeComponent();
        BindingContext = dataView;
        this.dataView = dataView;
        dataView.Age = 0;
        dataView.Name = string.Empty;
        dataView.Image = "john.png";
        entName.Focus();
    }

    private async void AddbtnIMage_Clicked(object sender, EventArgs e)
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

            await Toast.Make("Image Was Add", ToastDuration.Short, 14).Show();

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void AddbtnData_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (dataView.Name!=string.Empty && dataView.Image!=string.Empty && dataView.Age != 0)
            {
                var choose = await DisplayAlert("Choose", "Do You Want To Add It", "Yes", "No");

                if (choose == true)
                {
                    await dataView.AppDb.dataModels.AddAsync(new Models.DataModel
                    {
                        Name = dataView.Name,
                        Age = dataView.Age,
                        Image = dataView.Image,
                    });
                    await dataView.AppDb.SaveChangesAsync();
                    await Toast.Make($"{dataView.Name} Was Added", ToastDuration.Short, 14).Show();
                    dataView.Name = string.Empty;
                    dataView.Age = 0;
                    dataView.Image = "john.png";
                    await Task.Run(() => dataView.DataModels = dataView.AppDb.dataModels.ToList());
                    if (dataView.Msg != string.Empty)
                    {
                        dataView.Msg = string.Empty;
                    }
                    entAge.Unfocus();
                    await Navigation.PopAsync();
                }
                
            }
            else
            {
                throw new Exception("Name or image or Age Is Empty Now");
            }
        }catch(Exception ex)
        {
            await DisplayAlert("error", ex.Message, "Ok");
        }
    }
}