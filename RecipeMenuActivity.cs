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

namespace ELiquidLab
{
    [Activity(Label = "RecipeMenuActivity")]
    public class RecipeMenuActivity : Activity
    {
        private ListView recipeListView;
        private List<Recipe> allRecipes;
        private RecipeDataService recipeDataService;
         
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RecipeMenuView);

            recipeListView = FindViewById<ListView>(Resource.Id.recipeListView);
            recipeDataService = new RecipeDataService();
            allRecipes = recipeDataService.GetAllRecipes();

            recipeListView.Adapter = new RecipeListAdapter(this, allRecipes);
            recipeListView.FastScrollEnabled = true;

            recipeListView.ItemClick += RecipeListView_ItemClick;
        }

        private void RecipeListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var recipe = allRecipes[e.Position];

            var intent = new Intent();
            intent.SetClass(this, typeof (RecipeDetailActivity));
            intent.PutExtra("selectedRecipeId", recipe.RecipeId);

            StartActivityForResult(intent, 100); //Request #100
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok && requestCode == 100)
            {
                var selectedRecipe = recipeDataService.GetRecipeById(data.GetIntExtra("selectedRecipe", 1));

                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Confirmation");
                dialog.SetMessage("Building Recipe");
                dialog.Show();
            }
        }
    }
}