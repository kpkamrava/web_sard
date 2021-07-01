using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace web_lib
{
  public static  class FilesHelper
    {
        public static bool IsImage(this string exch)
        {
            var str = exch.ToLower();
            if (
                str.EndsWith(".jpg") ||


                 str.EndsWith(".jpeg") ||
                 str.EndsWith(".png") ||
                str.EndsWith(".jfif") ||
                 str.EndsWith(".ico")
                                )
            {
                return true;


            }
            return false;
        }
     

        //public static string typeFileToImage(this string type)
        //{      dynamic[] listtype = new dynamic[]{
        //     new   { aztype=".application",totype="exe" },
        //     new  {aztype=".app",totype="exe" },
        //     new  {aztype=".exe",totype="exe" },
        //     new  {aztype=".xls",totype="xls" },
        //      new {aztype=".pdf",totype="pdf" },
        //     new  {aztype=".docx",totype="doc" },
        //     new  {aztype=".txt",totype="txt" },
        //     new  {aztype=".bmp",totype="jpg" },
        //     new  {aztype=".jpg",totype="jpg" },
        //     new  {aztype=".jpeg",totype="jpg" },
        //    };
     
           
        //    var l = listtype.SingleOrDefault(a => a.aztype.ToLower() == type.ToLower());
        //    if (l != null)
        //    {
        //        return l.totype + ".jpg";
        //    }
        //    return "file.jpg";

        //}
        public static byte[] ToByteArray(this IFormFile file, bool chanesizetoSmall = false)
        {
            if (file == null)
            {
                return null;

            }
            byte[] image = null;
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                image = target.ToArray();
            }

            if (chanesizetoSmall)
            {
                image = image.imgTosmall();

            }
            return image;
        }
        public static string imgToBase64(this byte[] Imagedata)
        {
            if (Imagedata == null)
            {
                return null;
            }
            return "data:image/png;base64," + Convert.ToBase64String(Imagedata, 0, Imagedata.Length);
        }
        public enum imageSize
        {
            s100 = 100, s300 = 300, s350 = 350, s450 = 450, s800 = 800, s1080 = 1080
        }

        public static byte[] imgTosmall(this byte[] byteImageIn, imageSize tosize = imageSize.s800, long dpi = 72)
        {
            if (byteImageIn == null)
            {
                return null;
            }
            byte[] currentByteImageArray = byteImageIn;


            MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
            Image fullsizeImage = Image.FromStream(inputMemoryStream);


            double s = (double)(800 * 600);

            if (fullsizeImage.Width * fullsizeImage.Height > s)
            {
                double ww = (double)fullsizeImage.Width / (int)tosize;
                double hh = (double)fullsizeImage.Height / (int)tosize;

                var w = fullsizeImage.Width / hh;
                var h = fullsizeImage.Height / ww;




                Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(w), (int)(h)));
                MemoryStream resultStream = new MemoryStream();

                fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                currentByteImageArray = resultStream.ToArray();
                resultStream.Dispose();
                resultStream.Close();
            }

            return imgTosmallChengeDPI(currentByteImageArray, dpi);
        }
        public static byte[] imgTosmallChengeDPI(this byte[] byteImageIn, long dpi = 72)
        {
            if (byteImageIn == null)
            {
                return null;
            }

            byte[] currentByteImageArray = byteImageIn;


            using (MemoryStream inputMemoryStream = new MemoryStream(byteImageIn))
            {
                using (var bmpOutput = new Bitmap(inputMemoryStream))
                {
                    ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                    System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

                    var myEncoderParameters = new EncoderParameters(1);
                    var myEncoderParameter = new EncoderParameter(myEncoder, dpi /*10L*/);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    bmpOutput.SetResolution(dpi, dpi); // Change to any dpi 
                    MemoryStream resultStream = new MemoryStream();

                    bmpOutput.Save(resultStream, jgpEncoder, myEncoderParameters);
                    currentByteImageArray = resultStream.ToArray();
                    resultStream.Dispose();
                    resultStream.Close();

                }

            }
            return currentByteImageArray;
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }


        //
        // Summary:
        //    تصویر را در حافضه با سایز اصلی و سایزهای مختلف ذخیره میکند
       
        public static string SaveImageSizes(this byte[] Image, IHostingEnvironment _environment,string folder,string fileneme)
        {
            string path = _environment.WebRootPath;
            

            foreach (var item in folder.Split(new char[] {'/','\\' })) 
            {
                path= Path.Combine(path, item);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
          
            string fileExtension = ".jpg";

            if (fileneme.Contains("."))
            {
                fileExtension = "."+ fileneme.Split(".").Last();

                fileneme = fileneme.Remove(fileneme.Length - fileExtension.Length);

            }
            
           
            var pMain = Path.Combine(path, fileneme + fileExtension);
         //  File.WriteAllBytes(pMain, Image.imgTosmallChengeDPI());
            

            foreach (var item in imageSize.s100.GetAllItems().OrderByDescending(a=>a))
            {
                var p = "";
                if (item == imageSize.s1080)
                {
                    p = Path.Combine(path, fileneme + fileExtension);

                }
                else
                {
                    p = Path.Combine(path, fileneme + "-" + item.ToString() + fileExtension);

                }
                  File.WriteAllBytes(p, Image.imgTosmall(item)); 
            }
            var r=pMain.Remove(0, _environment.WebRootPath.Length);
            return r;
        }
        public static void ImageDeleteSizes(this string locationStore, IHostingEnvironment _environment)
        {
            var _locationStore = _environment.WebRootPath+ locationStore;
            if (File.Exists(_locationStore))
            {
                File.Delete(_locationStore);
               
            }
            foreach (var item in imageSize.s100.GetAllItems())
            {
                var url2 = _locationStore.Insert(_locationStore.Length - (".jpg").Length, "-" + item.ToString());
                if (File.Exists(url2))
                {
                    File.Delete(url2);
                } 

            }

        }

        public static string  ImageOtherSize(this string locationStore, imageSize seze)
        {
            var a = locationStore;
            if (a.EndsWith(".jpg"))
            {

              a=  a.Insert(a.Length - (".jpg").Length, "-" + seze.ToString());
            }
             
            return a;
        }


    }
}
