using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ELiquidLab.Utility;
using Java.IO;
using Android.Graphics;

namespace ELiquidLab
{
    [Activity(Label = "Take A Picture of your recipe")]
    public class TakePictureActivity : Activity
    {
        private ImageView recipePictureImageView;
        private Button takePictureButton;
        private File imageDirectory;
        private File imageFile;
        private Bitmap imageBitmap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.TakePictureView);

            FindViews();
            HandleEvents();

            imageDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                Android.OS.Environment.DirectoryPictures), "ELiquidLab");
            if (!imageDirectory.Exists())
                imageDirectory.Mkdirs();
        }


        private void FindViews()
        {
            recipePictureImageView = FindViewById<ImageView>(Resource.Id.recipePictureImageView);
            takePictureButton = FindViewById<Button>(Resource.Id.takePictureButton);
        }

        private void HandleEvents()
        {
            takePictureButton.Click += TakePictureButton_Click;
        }

        private void TakePictureButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            imageFile = new File(imageDirectory, String.Format("RecipePhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(imageFile));
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultcode, Intent data)
        {
            int height = recipePictureImageView.Height;
            int width = recipePictureImageView.Width;
            imageBitmap = ImageHelper.GetImageBitmapFromFilePath(imageFile.Path, width, height);

            if (imageBitmap != null)
            {
                recipePictureImageView.SetImageBitmap(imageBitmap);
                imageBitmap = null;
            }

            //Required to avoid memory leaks
            GC.Collect();
        }

    }
}