using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPDebugApp2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SingleFilePicker : Page
    {
        //MainPage rootPage = MainPage.Current;
       
        public SingleFilePicker()
        {
            this.InitializeComponent();
        }

        private async void PickAFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear previous returned file name, if it exists, between iterations of this scenario
            OutputTextBlock.Text = "";

            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".doc");
            openPicker.FileTypeFilter.Add(".docx");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                OutputTextBlock.Text = "Picked file: " + file.Name;

                OfficeToPdfService officeToPdfService = new OfficeToPdfService();
                string outPath = file.Path.Substring(0, file.Path.LastIndexOf(".")) + ".pdf";

                openPicker.FileTypeFilter.Add(".pdf");
                officeToPdfService.ConvertOfficeFile(file.Path, outPath);

                StorageFile newPdfFile = await openPicker.PickSingleFileAsync();
            }
            else
            {
                OutputTextBlock.Text = "Operation cancelled.";
            }
        }
    }
}
