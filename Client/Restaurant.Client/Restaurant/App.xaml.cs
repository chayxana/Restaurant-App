﻿using Akavache;
using ModernHttpClient;
using ReactiveUI;
using ReactiveUI.XamForms;
using Restaurant.Pages;
using Restaurant.Pages.MainPages;
using Restaurant.ReactiveUI;
using Restaurant.ViewModels;
using Restaurant.Views;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Xamarin.Forms;

namespace Restaurant
{
    public partial class App : Application
    {
        public App()
        {
            var app = new AppBotstrapper();
            MainPage = app.MainPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        public new static App Current
        {
            get
            {
                return (App)Application.Current;
            }
        }

        public ColorTheme GetThemeFromColor(string color)
        {
            return new ColorTheme
            {
                Primary = (Color)App.Current.Resources["{0}Primary".Fmt(color)],
                Light = (Color)App.Current.Resources["{0}Light".Fmt(color)],
                Dark = (Color)App.Current.Resources["{0}Dark".Fmt(color)],
            };
        }
    }

    public class AppBotstrapper : ReactiveObject, INavigatableScreen
    {
        // The Router holds the ViewModels for the back stack. Because it's
        // in this object, it will be serialized automatically.
        public NavigationState Navigation { get; protected set; }

        public AppBotstrapper()
        {
            Navigation = new NavigationState();

            Locator.CurrentMutable.RegisterConstant(this, typeof(INavigatableScreen));

            // Set up Akavache
            // 
            // Akavache is a portable data serialization library that we'll use to
            // cache data that we've downloaded
            BlobCache.ApplicationName = "Restaurant";

            // Set up Fusillade
            //
            // Fusillade is a super cool library that will make it so that whenever
            // we issue web requests, we'll only issue 4 concurrently, and if we
            // end up issuing multiple requests to the same resource, it will
            // de-dupe them. We're saying here, that we want our *backing*
            // HttpMessageHandler to be ModernHttpClient.
            Locator.CurrentMutable.RegisterConstant(new NativeMessageHandler(), typeof(HttpMessageHandler));

            Locator.CurrentMutable.Register(() => new AuthenticationPage(), typeof(IViewFor<AuthenticationViewModel>));

            Locator.CurrentMutable.Register(() => new SignUpPage(), typeof(IViewFor<SignUpViewModel>));

            Locator.CurrentMutable.Register(() => new MainPage(), typeof(IViewFor<MainViewModel>));
        }

        public Page MainPage()
        {
            return null;
        }
    }

    public class ColorTheme
    {
        public Color Primary
        {
            get;
            set;
        }

        public Color Light
        {
            get;
            set;
        }

        public Color Dark
        {
            get;
            set;
        }

        public Color Medium
        {
            get;
            set;
        }

        public Color PrimaryText
        {
            get;
            set;
        }
    }

    public static class Extensions
    {
        public static string Fmt(this string s, params object[] args)
        {
            return string.Format(s, args);
        }
    }
}