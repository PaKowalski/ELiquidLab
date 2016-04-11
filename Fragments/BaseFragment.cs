using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ELiquidLab.Core.Model;
using ELiquidLab.Core.Service;

namespace ELiquidLab.Fragments
{
    public class BaseFragment : Fragment
    {
        protected ListView listView;
        protected RecipeDataService recipeDataService;
        protected List<Recipe> recipes;

        public BaseFragment()
        {
            recipeDataService = new RecipeDataService();
        }

        protected void HandleEvents()
        {
            listView.ItemClick += ListView_ItemClick;
        }
        protected void FindViews()
        {
            listView = this.View.FindViewById<ListView>(Resource.Id.recipeListView);
        }

        protected void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var recipe = recipes[e.Position];

            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(RecipeDetailActivity));
            intent.PutExtra("selectedRecipeId", recipe.RecipeId);

            StartActivityForResult(intent, 100);
        }
    }
}