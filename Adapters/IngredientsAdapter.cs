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
using Object = Java.Lang.Object;

namespace ELiquidLab.Adapters
{
    public class IngredientsAdapter : BaseAdapter
    {
        private Activity context;
        private Dictionary<string, double> dict;

        public IngredientsAdapter(Activity context, Dictionary<string, double> dict)
        {
            this.dict = dict;
            this.context = context;
        } 
        public override Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = dict.ToString();
            return view;
        }

        public override int Count
        {
            get { return dict.Count;  }
            
        }
    }
}