﻿using ForumDEG.Interfaces;
using ForumDEG.Views;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using System.Diagnostics;
using ForumDEG.Views.Forms;

namespace ForumDEG.ViewModels {
    class AppMasterViewModel : INotifyPropertyChanged {
        public ICommand HomeClickedCommand { get; private set; }
        public ICommand ForumsClickedCommand { get; private set; }
        public ICommand UsersClickedCommand { get; private set; }
        public ICommand FormsClickedCommand { get; private set; }
        public ICommand NewForumClickedCommand { get; private set; }
        public ICommand RegisterUserClickedCommand { get; private set; }
        public ICommand NewFormClickedCommand { get; private set; }
        public ICommand PlusButtonClickedCommand { get; set; }
        public ICommand CoodinatorViewCommand { get; set; }
        public ICommand LogoutClickedCommand { get; set; }

        private float TapCount = 0;
        
        public float _tapCount {
            get {
                return _tapCount;
            }
            set {
                if (_tapCount != value)
                    _tapCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TapCount"));
            }
        }

        private bool _extraButtonsVisibility;

        public bool ExtraButtonsVisibility {
            get {
                return _extraButtonsVisibility;
            }
            set {
                if (_extraButtonsVisibility != value) {
                    _extraButtonsVisibility = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ExtraButtonsVisibility"));
                }
            }
        }

        private readonly IPageService _pageService;

        public event PropertyChangedEventHandler PropertyChanged;

        public AppMasterViewModel(IPageService pageService) {
            
            _pageService = pageService;

            HomeClickedCommand = new Command(async () => await HomeClicked());
            ForumsClickedCommand = new Command(async () => await ForumsClicked());
            UsersClickedCommand = new Command(async () => await UsersClicked());
            FormsClickedCommand = new Command(async () => await FormsClicked());
            NewForumClickedCommand = new Command(async () => await NewForumClicked());
            RegisterUserClickedCommand = new Command(async () => await RegisterUserClicked());
            NewFormClickedCommand = new Command(async () => await NewFormClicked());
            LogoutClickedCommand = new Command(async () => await LogoutClicked());
            PlusButtonClickedCommand = new Command(async () => await PlusButtonClicked());

            ExtraButtonsVisibility = false;
            TapCount = 0;
        }

        private async Task HomeClicked() {
            TapCount = 0;
            ExtraButtonsVisibility = false;
            await _pageService.PushAsync(new AppMasterPage());
        }

        private async Task ForumsClicked() {
            TapCount = 0;
            ExtraButtonsVisibility = false;
            await _pageService.PushAsync(new ForumsPage());
        }
        
        private async Task UsersClicked() {
            TapCount = 0;
            ExtraButtonsVisibility = false;
            await _pageService.PushAsync(new UsersPage());
        }

        private async Task FormsClicked() {
            TapCount = 0;
            ExtraButtonsVisibility = false;
            await _pageService.PushAsync(new FormsPage());
        }

        private async Task NewForumClicked() {
            TapCount++;
            if (TapCount % 2 == 0) {
                ExtraButtonsVisibility = false;
            }
            await _pageService.PushAsync(new NewForumPage());
        }

        private async Task RegisterUserClicked() {
            TapCount++;
            if (TapCount % 2 == 0) {
                ExtraButtonsVisibility = false;
            }
            await _pageService.PushAsync(new UserRegistrationPage());
        }

        private async Task NewFormClicked() {
            TapCount++;
            if (TapCount % 2 == 0) {
                ExtraButtonsVisibility = false;
            }
            await _pageService.PushAsync(new Views.Forms.NewFormPage());
        }

        private async Task LogoutClicked() {
            Debug.WriteLine("[AppMasterViewModel]: Logout clicked");
            Helpers.Settings.IsUserLogged = false;
            await _pageService.PushAsync(new LoginPage());
        }

        private async Task PlusButtonClicked() {
            TapCount++;
            if (TapCount % 2 == 0) {
                ExtraButtonsVisibility = false;
            }
            else {
                ExtraButtonsVisibility = true;
            }
        }
    }
}
