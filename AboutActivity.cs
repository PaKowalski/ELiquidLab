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
    [Activity(Label = "About E-Liquid Lab")]
    public class AboutActivity : Activity
    {
        private TextView emailTextView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AboutView);
            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            emailTextView = FindViewById<TextView>(Resource.Id.emailTextView);
        }

        private void HandleEvents()
        {
            emailTextView.Click += EmailTextView_Click;
        }

        private void EmailTextView_Click(object sender, EventArgs e)
        {

            var intent = new Intent(Intent.ActionSend);
            intent.PutExtra(Intent.ExtraEmail, emailTextView.Text);
            intent.PutExtra(Intent.ExtraSubject, "About E-Liquid Lab");
            intent.SetType("message/rfc822");
            StartActivity(intent);
        }
    }
}