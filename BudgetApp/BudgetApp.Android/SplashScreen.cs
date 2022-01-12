﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BudgetApp.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
                MainLauncher = true,
       NoHistory = true,
        Icon = "@drawable/mybudget")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            System.Threading.Thread.Sleep(500);
            StartActivity(typeof(MainActivity));
            
        }
    }
}