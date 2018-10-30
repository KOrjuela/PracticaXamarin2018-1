namespace App.ViewModel
{
    using BaseViewModels.Utility;
    using GalaSoft.MvvmLight.Command;
    using global::App.Helpers;
    using global::App.Model.Entities;
    using global::App.Resources;
    using global::App.Services;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class RegisterViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private ImageSource imageSource;
        private MediaFile file;
        #endregion

        #region Properties
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.UpdateValueProperty(ref this.imageSource, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.UpdateValueProperty(ref this.isEnabled, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.UpdateValueProperty(ref this.isRunning, value); }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string Password { get; set; }

        public string Confirm { get; set; }
        #endregion

        #region Constructors
        public RegisterViewModel()
        {
            this.apiService = new ApiService();

            this.IsEnabled = true;
            this.ImageSource = "no_image";
        }
        #endregion

        #region Methods
        #endregion 

        #region Action
        public ICommand ActionRegister
        {
            get
            {
                return new RelayCommand(EventRegister);
            }
        }


        public ICommand ActionAddImage
        {
            get
            {
                return new RelayCommand(EventAddImage);
            }
        }

        #endregion

        #region Event
        private async void EventRegister()
        {
            if (string.IsNullOrEmpty(this.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "You must enter a first name.",
                    Languages.Accept
                );

                return;
            }

            if (string.IsNullOrEmpty(this.LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "You must enter a last name.",
                    Languages.Accept
                );

                return;
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept
                );

                return;
            }

            if (!RegularExpression.IsValidEmail(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "You must enter a valid email.",
                    Languages.Accept
                );

                return;
            }

            if (string.IsNullOrEmpty(this.Telephone))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "You must enter a phone.",
                    Languages.Accept
                );

                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "The password must have at least seix (6) characters.",
                    Languages.Accept
                );

                return;
            }

            if (this.Password.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "The password must have at least seix (6) characters.",
                    Languages.Accept
                );

                return;
            }

            if (string.IsNullOrEmpty(this.Confirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "You must enter a confirm.",
                    Languages.Accept
                );

                return;
            }

            if (this.Password != this.Confirm)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "The password and confirm does not match.",
                    Languages.Accept
                );

                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var checkConnetion = await this.apiService.CheckConnection();

            if (!checkConnetion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept
                );

                return;
            }

            byte[] imageArray = null;

            if (this.file != null)
            {
                imageArray = Files.ReadFully(this.file.GetStream());
            }

            var user = new User
            {
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Telephone = this.Telephone,
                ImageArray = imageArray,
                UserTypeId = 1,
                Password = this.Password,
            };

            var apiLandsAzure = Application.Current.Resources["APILandsAzure"].ToString();

            var response = await this.apiService.Post(
                    apiLandsAzure,
                    "/api",
                    "/Users",
                    user
                );

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept
                );

                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                "Confirm",
                "The user was create, now you can login with this email and password.",
                Languages.Accept
            );

            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void EventAddImage()
        {
            try
            {


                ///Inicializamos lña libreria
                await CrossMedia.Current.Initialize();

                /// Tiene Galeriay Fotos
                if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                {
                    /// Mostramod lista de opciones para cargar la Imagen
                    var source = await Application.Current.MainPage.DisplayActionSheet(
                        "SourceImageQuestion",
                        "Cancel",
                        null,
                        "From gallery",
                        "From camera"
                    );

                    if (source == "Cancel")
                    {
                        this.file = null;
                        return;
                    }

                    if (source == "From camera")
                    {
                        /// lanzamos la camara
                        this.file = await CrossMedia.Current.TakePhotoAsync(
                            new StoreCameraMediaOptions
                            {
                                Directory = "Sample",
                                Name = "test.jpg",
                                PhotoSize = PhotoSize.Small,
                            }
                        );
                    }
                    else
                    {
                        /// Lanzamos la galeria 
                        this.file = await CrossMedia.Current.PickPhotoAsync();
                    }
                }
                else
                {
                    this.file = await CrossMedia.Current.PickPhotoAsync();
                }

                if (this.file != null)
                {
                    this.ImageSource = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        return stream;
                    });
                }
            }
            catch (System.Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
                return;
            }
        }
        #endregion
    }
}
