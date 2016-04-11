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
using ELiquidLab.Fragments;

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

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            /*

            recipeListView = FindViewById<ListView>(Resource.Id.recipeListView);
            recipeDataService = new RecipeDataService();
            allRecipes = recipeDataService.GetAllRecipes();

            recipeListView.Adapter = new RecipeListAdapter(this, allRecipes);
            recipeListView.FastScrollEnabled = true;

            recipeListView.ItemClick += RecipeListView_ItemClick;
            */
            AddTab("Favorites", new FavoriteRecipeFragment());
            AddTab("Desserts", new DessertsFragment());
            AddTab("Cereals", new CerealsFragment());

        }

        private void AddTab(string tabText, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);

            tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };

            tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);
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
                var selectedRecipe = recipeDataService.GetRecipeById(data.GetIntExtra("selectedRecipeId", 0));

                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Confirmation");
                dialog.SetMessage(string.Format("You've added {0} time(s) the {1}", data.GetIntExtra("amount", 0), selectedRecipe.Name));
                dialog.Show();
            }
        }
    }
}