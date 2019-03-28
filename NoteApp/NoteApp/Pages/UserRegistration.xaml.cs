namespace NoteApp.Pages
{
    using NoteApp.Repository;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// User registration form
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserRegistration : ContentPage
	{
		public UserRegistration ()
		{
			InitializeComponent ();
		}
        UserRepository userRepository = new UserRepository();

        /// <summary>
        /// Handles the AddClicked event of the Btn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Btn_AddClicked(Object sender, EventArgs e)
        {
            /// calls the use repository to add user
            await userRepository.AddUserAsync(firstName.Text, lastName.Text, email.Text, password.Text);
            /// clears the field on successful addition of user
            firstName.Text = string.Empty;
            lastName.Text = string.Empty;
            email.Text = string.Empty;
            password.Text = string.Empty;
            /// displays message on successful addition of user
            await DisplayAlert("Success", "Person Added Successfully", "OK");
        }

        private async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Login());
        }
    }
}