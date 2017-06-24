﻿using System;
using ForumDEG.Utils;
using ForumDEG.Views;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.ComponentModel;
using ForumDEG.Interfaces;

namespace ForumDEG.ViewModels {
    public class ForumDetailViewModel : PageService, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IPageService _pageService;
        private readonly Helpers.Forum _forumService;

        private string _buttonText;
        private Color _buttonColor;
        private bool _isConfirmed;
        public bool IsConfirmed {
            get {
                return _isConfirmed;
            }
            set {
                _isConfirmed = value;
            }
        }

        private Helpers.Coordinator coordinatorService;

        /* Forum properties */

        public string Title { get; set; }
        public string Place { get; set; }
        public string Schedules { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Hour { get; set; }
        public bool IsPast { get; private set; }
        public int Registration { get; set; }
        public string RemoteId { get; set; }
        public string Confirmations { get; set; }

        public bool IsCurrentUserAdmin => Helpers.Settings.IsUserAdmin;
        public bool IsCurrentUserCoord => Helpers.Settings.IsUserCoord;

        /* Button UI properties */
        public string ButtonText {
            get {
                return _buttonText;
            }
            set {
                if (_buttonText != value) {
                    _buttonText = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ButtonText"));
                }
            }
        }

        public Color ButtonColor {
            get {
                return _buttonColor;
            }
            set {
                if (_buttonColor != value) {
                    _buttonColor = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ButtonColor"));
                }
            }
        }

        private bool _activityIndicator = false;
        public bool ActivityIndicator {
            get {
                return _activityIndicator;
            }
            set {
                if (_activityIndicator != value) {
                    _activityIndicator = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActivityIndicator"));
                }
            }
        }

        public ICommand PresenceCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public ForumDetailViewModel(IPageService pageService) {
            ActivityIndicator = false;
            _pageService = pageService;
            EditCommand = new Command(EditForum);
            PresenceCommand = new Command(HandlePresence);
            DeleteCommand = new Command(DeleteForum);
            coordinatorService = new Helpers.Coordinator();
            IsPast = HasPassed();
            _forumService = new Helpers.Forum();

            //GetConfirmation();
        }

        public async void GetConfirmation() {
            ActivityIndicator = true;
            _isConfirmed = await coordinatorService.GetConfirmationStatusAsync(Helpers.Settings.UserReg, RemoteId);
            HandleButtonUI();
            ActivityIndicator = false;
        }

        public void HandleButtonUI() {
            ActivityIndicator = true;
            if (_isConfirmed) {
                Debug.WriteLine("[ForumDetailVM]: isConfirmed true");
                ButtonText = "Cancelar presença";
                ButtonColor = Color.Red;
            }
            else {
                Debug.WriteLine("[ForumDetailVM]: isConfirmed false, " + Title + " forum");
                ButtonText = "Confirmar presença";
                ButtonColor = Color.Orange;
            }
            ActivityIndicator = false;
        }

        public bool HasPassed() {
            return DateTime.Now > Date;
        }

        public void HandlePresence() {
            ActivityIndicator = true;
            Debug.WriteLine("[ForumDetailVM]: Inside Presence Handler");
            if (!_isConfirmed) {
                coordinatorService.PostConfirmationStatusAsync(Helpers.Settings.UserReg, RemoteId);
            }
            else {
                coordinatorService.DeleteConfirmationAsync(Helpers.Settings.UserReg, RemoteId);
            }

            TogglePresence();
            HandleButtonUI();
            ActivityIndicator = false;
        }

        public void TogglePresence() {
            ActivityIndicator = true;
            _isConfirmed = !_isConfirmed;
            ActivityIndicator = false;
        }

        private async void EditForum() {
            Debug.WriteLine(" EDITAR FORUM ");
            ActivityIndicator = true;
            await PushAsync(new ForumEditPage(RemoteId));
            ActivityIndicator = false;
        }

        private async void DeleteForum() {
            var answer = await _pageService.DisplayAlert("Deletar Fórum", "Tem certeza que deseja deletar o fórum existente? Esta ação não poderá ser desfeita.", "Sim", "Não");
            Debug.WriteLine("Answer: " + answer);
            if (answer == true) {
                ActivityIndicator = true;
                if (await _forumService.DeleteForumAsync(RemoteId)) {
                    ActivityIndicator = false;
                    await _pageService.DisplayAlert("Fórum deletado", "O fórum foi excluído com sucesso", null ,"OK" );
                    await _pageService.PopToRootAsync();
                }
                else {
                    ActivityIndicator = false;
                    await _pageService.DisplayAlert("Erro!", "O fórum não pôde ser deletado, tente novamente.", "OK", "CANCELAR");
                }
            }
            ActivityIndicator = false;
        }
    }
}
