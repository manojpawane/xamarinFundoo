namespace NoteApp.Pages
{
    using NoteApp.Interfaces;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Sends the reset password mail with respective to email id passed
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgetPasswordEmail : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForgetPasswordEmail"/> class.
        /// </summary>
        public ForgetPasswordEmail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Clicked(object sender, EventArgs e)
        {
            /// Calls run time service from respective device (Android, iOS, UWP) to send request to reset password
             DependencyService.Get<IFirebaseAuthenticator>().SendResetPasswordMail(email.Text);
        }
    }
}