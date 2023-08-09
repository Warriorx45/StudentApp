using CommunityToolkit.Maui.Storage;

namespace TestApp.Views;

public partial class MovePage : ContentPage
{
	public MovePage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            await CheckAndRequestStoragePermission();
            var folder = await FolderPicker.PickAsync(default);
            mylabel.Text = folder.Folder.Path;
        }
        catch
        {
            
        }
    }

    private async Task CheckAndRequestStoragePermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        var statu = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.StorageRead>();
            statu = await Permissions.RequestAsync<Permissions.StorageWrite>();

            if (status != PermissionStatus.Granted && statu != PermissionStatus.Granted)
            {
                var choose = await Shell.Current.DisplayAlert("Error", "I Want This Permission For Get DataBase", "Ok", "No");
                if (choose)
                {
                    status = await Permissions.RequestAsync<Permissions.StorageRead>();
                    statu = await Permissions.RequestAsync<Permissions.StorageWrite>();
                }
                else
                {
                    App.Current.Quit();
                }
            }
        }


    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            string path1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Database.db");
            string path2 = Path.Combine(mylabel.Text, "Database.db");

            await Task.Run(() => File.Copy(path1, path2));

            await Shell.Current.DisplayAlert("", "Moved Successfully", "OK");
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}