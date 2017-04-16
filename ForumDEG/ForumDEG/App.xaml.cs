﻿using ForumDEG.Utils;
using ForumDEG.Views;
using Xamarin.Forms;

namespace ForumDEG {
    public partial class App : Application {
         
        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.NewForumPage());
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
