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
using ELiquidLab.Adapters;
using ELiquidLab.Core.Model;
using ELiquidLab.Core.Service;
using ELiquidLab.Utility;

namespace ELiquidLab
{
    [Activity(Label = "Recipe Detail")]
    public class RecipeDetailActivity : Activity
    {
        private ImageView recipeImageView;
        private TextView recipeNameTextView;
        private TextView descriptionTextView;
        private Button cancelButton;
        private Button createButton;

        private Recipe selectedRecipe;
        private RecipeDataService dataservice;

        private Dictionary<string, double> ingredientsDictionary; 
        private ListView ingredientsListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RecipeDetailView);

            RecipeDataService dataService = new RecipeDataService();

            var selectedRecipeId = Intent.Extras.GetInt("selectedRecipeId");
            selectedRecipe = dataService.GetRecipeById(selectedRecipeId);
            ingredientsDictionary = dataService.GetRecipeById(selectedRecipeId).Ingredients; 

            ingredientsListView = FindViewById<ListView>(Resource.Id.ingredientsListView);
            ingredientsListView.Adapter = new IngredientsAdapter(this, ingredientsDictionary);

            FindViews();
            BindData();
            HandleEvents();

        }

        private void FindViews()
        {
            recipeImageView = FindViewById<ImageView>(Resource.Id.recipeImageView);
            recipeNameTextView = FindViewById<TextView>(Resource.Id.recipeNameTextView);
            descriptionTextView = FindViewById<TextView>(Resource.Id.descriptionTextView);
            cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            createButton = FindViewById<Button>(Resource.Id.createButton);
        }

        private void BindData()
        {
            recipeNameTextView.Text = selectedRecipe.Name;
            descriptionTextView.Text = selectedRecipe.Description;

            var imageBitmap = ImageHelper.GetImageBitmapFromUrl("http://imgur.com/iLKGjqg" + ".jpg");
            recipeImageView.SetImageBitmap(imageBitmap);

            var ingredients = selectedRecipe.Ingredients;

            //ingredientsListView.Adapter
        }

        private void HandleEvents()
        {
            createButton.Click += CreateButton_Click;
            cancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            //This should take you to the CreateRecipe Activity

            /*
            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Confirmation");
            dialog.SetMessage("Building " + recipeNameTextView.Text + "!");
            dialog.Show();
            */
            var intent = new Intent();
            intent.PutExtra("selectedRecipeId", selectedRecipe.RecipeId);

            SetResult(Result.Ok, intent);
            this.Finish();
        }

    }
}