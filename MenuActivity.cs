using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ELiquidLab
{
    [Activity(Label = "MenuActivity", MainLauncher = true)]
    public class MenuActivity : Activity
    {
        private Button createButton;
        private Button viewRecipesButton;
        private Button aboutButton;
        private Button takePictureButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainMenu);

            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            createButton = FindViewById<Button>(Resource.Id.createButton);
            viewRecipesButton = FindViewById<Button>(Resource.Id.viewRecipesButton);
            aboutButton = FindViewById<Button>(Resource.Id.aboutButton);
            takePictureButton = FindViewById<Button>(Resource.Id.takePictureButton);

        }

        private void HandleEvents()
        {
            viewRecipesButton.Click += ViewRecipesButton_Click;
            aboutButton.Click += AboutButton_Click;
            takePictureButton.Click += TakePictureButton_Click;
        }

        private void ViewRecipesButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RecipeMenuActivity));
            StartActivity(intent);
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AboutActivity));
            StartActivity(intent);
        }

        private void TakePictureButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(TakePictureActivity));
            StartActivity(intent);
        }
    }
}