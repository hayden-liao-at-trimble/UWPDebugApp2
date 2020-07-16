using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using pdftron.PDF;
using pdftron.SDF;
using Windows.UI.Xaml.Controls;

namespace UWPDebugApp2
{
    class OfficeToPdfService
    {
        public async Task ConvertOfficeFile(string inFilePath, string outFilePath)
        {
            string errText = "NONE";
            try
            {
                PDFDoc pdfDoc = new PDFDoc();

                OfficeToPDFOptions options = null;
                //string pathToPdfTronFontResources = "C:\\Users\\hliao\\Documents\\Github\\FieldMobileApp\\App\\Field.Mobile\\Field.Mobile.Droid\\obj\\Debug\\100\\designtime\\Resource.designer.cs";
                //options.SetLayoutResourcesPluginPath(pathToPdfTronFontResources);

                pdftron.PDF.Convert.OfficeToPDF(pdfDoc, inFilePath, options);
                await pdfDoc.SaveAsync(outFilePath, SDFDocSaveOptions.e_remove_unused);
            }
            catch (Exception ex)
            {
                pdftron.Common.PDFNetException pdfNetEx = new pdftron.Common.PDFNetException(ex.HResult);
                if (pdfNetEx.IsPDFNetException)
                {
                    errText = string.Format("Exeption at line {0} in file {1}", pdfNetEx.LineNumber, pdfNetEx.FileName);
                    errText += Environment.NewLine;
                    errText += string.Format("Message: {0}", pdfNetEx.Message);
                }
                else
                {
                    errText = ex.ToString();
                }
            }
            if (!errText.Equals("NONE"))
            {
                //look at the error text
                var x = errText;
            }
        }
    }
}

/*
 using System;
using System.IO;

using pdftron.SDF;
using pdftron.PDF;

using Field.Mobile.Common.Services;
using Field.Mobile.Common.Services.Interfaces;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using pdftron.Filters;
using Windows.Foundation.Metadata;
using Microsoft.AppCenter;
using Windows.UI.Popups;

namespace Field.Mobile.UWP.Services
{
    public class OfficeToPdfService : IOfficeToPdfService
    {
        public string DirectoryPath => Path.Combine(ApplicationData.Current.LocalFolder.Path, Common.Constants.BackgroundDownloadDirectoryConstants.OfflineFiles);

        public async Task ConvertOfficeFile(string inFilePath, string outFilePath)
        {
            string errText = "NONE";
            try
            {
                PDFDoc pdfDoc = new PDFDoc();

                OfficeToPDFOptions options = new OfficeToPDFOptions();
                string pathToPdfTronFontResources = "C:\\Users\\hliao\\Documents\\Github\\FieldMobileApp\\App\\Field.Mobile\\Field.Mobile.Droid\\obj\\Debug\\100\\designtime\\Resource.designer.cs";
                options.SetLayoutResourcesPluginPath(pathToPdfTronFontResources);
                
                pdftron.PDF.Convert.OfficeToPDF(pdfDoc, inFilePath, options);
                await pdfDoc.SaveAsync(outFilePath, SDFDocSaveOptions.e_remove_unused);
            }
            catch (Exception ex)
            {
                pdftron.Common.PDFNetException pdfNetEx = new pdftron.Common.PDFNetException(ex.HResult);
                if (pdfNetEx.IsPDFNetException)
                {
                    errText = string.Format("Exeption at line {0} in file {1}", pdfNetEx.LineNumber, pdfNetEx.FileName);
                    errText += Environment.NewLine;
                    errText += string.Format("Message: {0}", pdfNetEx.Message);
                }
                else
                {
                    errText = ex.ToString();
                }
            }
            Logger logger = new Logger();
            logger.Log("\nERROR: " + errText);
        }

        [RemoteAsync]

        public async Task<bool> WordToPdfAsync(String input_path, String input_filename, String output_filename)
        {
            return false;
            //// Start with a PDFDoc (the conversion destination)
            //using (PDFDoc pdfDoc = new PDFDoc())
            //{
            //    var copyPath = GetAssetTempFileAsync(input_path, input_filename);
            //    // perform the conversion with no optional parameters
            //    await pdftron.PDF.Convert.OfficeToPDF(pdfDoc, copyPath, null);

            //    // save the result
            //    await pdfDoc.SaveAsync(CreateExternalFile(output_filename), SDFDocSaveOptions.e_linearized);

            //    Console.WriteLine("Saved " + output_filename);

            //    var docToOpen = new pdftron.PDF.PDFDoc(input_path + "/" + output_filename);
            //    if (docToOpen != null)
            //    {
            //        return true;
            //    }
            //}
            //return false;
        }

        //public static Stream GetAssetStream(string filePath)
        //{
        //    //try
        //    //{
        //    //    //TODO: figure out how to get a stream in UWP from a filepath 
        //    //    return StorageFile.GetFileFromPathAsync(filePath);
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    Console.WriteLine(e.Message);
        //    //    return null;
        //    //}

        //    //StorageFile initFile = await GetStorageFile(filePath);
        //    //var stream = await initFile.OpenAsync(FileAccessMode.ReadWrite);
        //    //return stream;
        //    throw NotImplementedException;
        //}

        public async Task<StorageFile> GetStorageFile(string filePath)
        {
            try
            {
                return await StorageFile.GetFileFromPathAsync(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<string> GetAssetTempFileAsync(string filePath, string fileName)
        {
            StorageFile initFile = await GetStorageFile(filePath + "/" + fileName);
            StorageFolder parentFolder = await initFile.GetParentAsync();
            var newFileName = "newFile";
            var newFile = await initFile.CopyAsync(parentFolder, newFileName);
            return newFile.Path;
        }

        public async Task<string> CreateExternalFile(string fileName)
        {
            return null;
            ////TODO: figure out how to get a stream in UWP from a filepath 
            //return new Java.IO.File(Android.App.Application.Context.GetExternalFilesDir(null), fileName).AbsolutePath;

            ////StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            ////StorageFile sampleFile = await storageFolder.CreateFileAsync("sample.txt", CreateCollisionOption.ReplaceExisting);
            ////return fileName;

            //StorageFile file = new StorageFile();
            //file.GetFileFromPathAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
 */
