using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Adeliate.Web
{
  public class ImageUploadHelper
  {
    private bool _hasError = false;
    private string _errorMessage = string.Empty;
    private int _maxHeight = -1;
    private int _maxWidth = -1;
    private string _extension = string.Empty;
    private Adlite.Data.Image _imageData = null;
    private Image _image = null;
    private HttpPostedFileBase _file = null;

    public bool HasError { get { return this._hasError; } }
    public string ErrorMessage { get { return this._errorMessage; } }
    public int ID { get { return this._imageData != null ? this._imageData.ID : -1; } }
    public Adlite.Data.Image ImageData { get { return this._imageData; } }
    
    public ImageUploadHelper(HttpPostedFileBase fileUploaded, int maxHeight, int maxWidth)
    {
      this._file = fileUploaded;
      this._maxHeight = maxHeight;
      this._maxWidth = maxWidth;

      if (this._file == null && this._file.ContentLength == 0)
      {
        this._hasError = true;
        this._errorMessage = "No file object passed";
        return;
      }
      
      this._extension = this._file.FileName.Split('.').LastOrDefault();
      if (!this.ValidateExtension())
      {
        this._hasError = true;
        this._errorMessage = "Wrong extension";
        return;
      }

      this._image = Image.FromStream(this._file.InputStream);
      if(this._image == null)
      {
        this._hasError = true;
        this._errorMessage = "Could not create memory stream";
        return;
      }

      this.Scale(maxWidth, maxHeight);

      //if(this._image.Height > this._maxHeight)
      //{
      //  double scale = ((this._image.Height * 1.0) / (this._maxHeight * 1.0));
      //  int newWidth = (int)Math.Ceiling((this._image.Width * 1.0) / scale);
      //  this._image = this.Scale(newWidth, this._maxHeight);
      //}
      //else if(this._image.Width > this._maxWidth)
      //{
      //  double scale = ((this._image.Width * 1.0) / (this._maxWidth* 1.0));
      //  int newHeight = (int)Math.Ceiling((this._image.Height* 1.0) / scale);
      //  this._image = this.Scale(this._maxWidth, newHeight);
      //}

      byte[] data = this.ImageToByteArray(this._image);
      this._imageData = new Adlite.Data.Image(-1, data, data.Length, this._image.Width, this._image.Height, this.GetMimeType(), this._extension, DateTime.Now, DateTime.Now);
      this._imageData.Insert();
    }

    private Image Scale(int Width, int Height)
    {
      Image imgPhoto = this._image;
      float sourceWidth = imgPhoto.Width;
      float sourceHeight = imgPhoto.Height;
      float destHeight = 0;
      float destWidth = 0;
      int sourceX = 0;
      int sourceY = 0;
      int destX = 0;
      int destY = 0;

      // force resize, might distort image
      if (Width != 0 && Height != 0)
      {
        destWidth = Width;
        destHeight = Height;
      }
      // change size proportially depending on width or height
      else if (Height != 0)
      {
        destWidth = (float)(Height * sourceWidth) / sourceHeight;
        destHeight = Height;
      }
      else
      {
        destWidth = Width;
        destHeight = (float)(sourceHeight * Width / sourceWidth);
      }

      Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
                                  PixelFormat.Format32bppPArgb);
      bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

      Graphics grPhoto = Graphics.FromImage(bmPhoto);
      grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

      grPhoto.DrawImage(imgPhoto,
          new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
          new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
          GraphicsUnit.Pixel);

      grPhoto.Dispose();
      return bmPhoto;
    }

    private byte[] ImageToByteArray(System.Drawing.Image imageIn)
    {
      MemoryStream ms = new MemoryStream();
      imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
      return ms.ToArray();
    }

    private Image ByteArrayToImage(byte[] byteArrayIn)
    {
      MemoryStream ms = new MemoryStream(byteArrayIn);
      Image returnImage = Image.FromStream(ms);
      return returnImage;
    }

    private bool ValidateExtension()
    {
      this._extension = this._extension.ToLower();
      switch (this._extension)
      {
        case "jpg":
          return true;
        case "png":
          return true;
        case "jpeg":
          return true;
        default:
          return false;
      }
    }

    private string GetMimeType()
    {
      return ExtensionHelper.GetMimeType(this._extension);
    }

  }
}
