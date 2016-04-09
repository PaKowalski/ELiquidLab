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
using ELiquidLab.Core.Model;
using ELiquidLab.Utility;

namespace ELiquidLab.Adapters
{
    public class RecipeListAdapter : BaseAdapter<Recipe>
    {
        List<Recipe> items;
        Activity context;

        public RecipeListAdapter(Activity context, List<Recipe> items) : base()
        {
            this.context = context;
            this.items = items;
        }


        public override long GetItemId(int position)
        {
            return position;
        }
        
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            /*
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item.Name;
            */

            var imageBitmap = ImageHelper.GetImageBitmapFromUrl("http://i.imgur.com/iLKGjqg.jpg");
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(
                    Resource.Layout.RecipeRowView, null);
            }
            convertView.FindViewById<TextView>(Resource.Id.recipeNameTextView).Text = item.Name;
            convertView.FindViewById<TextView>(Resource.Id.descriptionTextView).Text = item.Description;
            convertView.FindViewById<ImageView>(Resource.Id.recipeImageView).SetImageBitmap(imageBitmap);

            return convertView;
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override Recipe this[int position]
        {
            get { return items[position]; }
        }
    }
}