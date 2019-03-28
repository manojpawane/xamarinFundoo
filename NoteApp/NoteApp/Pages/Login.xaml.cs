namespace NoteApp.Pages
{
    using NoteApp.Interfaces;
    using NoteApp.Repository;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Contains method for user login with respect to device
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login ()
		{
            
            var response = DependencyService.Get<IFirebaseAuthenticator>().User();
            
            /// if in response user gets it will navigate to main page or else to login page
            if (response != null)
            {
                 Navigation.PushModalAsync(new MainPage());
            }
            else
            {
                InitializeComponent();
            }
        }

        /// <summary>
        /// The user repository
        /// </summary>
        UserRepository userRepository = new UserRepository();

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                /// Calls run time service from respective device (Android, iOS, UWP) to send request for USER login
                var response = await DependencyService.Get<IFirebaseAuthenticator>().LoginWithEmailPassword(email.Text, password.Text);
                
                /// if in response user exists than application will be navigate to main page
                if (response != null)
                {
                    await Navigation.PushModalAsync(new MainPage());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Handles the 1 event of the Button_Clicked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked_1(object sender, EventArgs e)
        {   
            /// will navigate to page where user have to enter the email to request for reset password
            await Navigation.PushAsync(new ForgetPasswordEmail());
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserRegistration());
        }
    }
}