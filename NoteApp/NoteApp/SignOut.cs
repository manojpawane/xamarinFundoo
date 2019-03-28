namespace NoteApp
{
    using NoteApp.Interfaces;
    using NoteApp.Pages;
    using Xamarin.Forms;

    /// <summary>
    /// Sign out user
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public class SignOut : ContentPage
    {

        protected async override void OnAppearing()
        {
            /// Calls run time service from respective device (Android, iOS, UWP) to sign out the user
            var response =  DependencyService.Get<IFirebaseAuthenticator>().SignOut();

            /// if signout fails than it will maintain user to be at same place
            if(response != null)
            {
                await Navigation.PushModalAsync(new MainPage());
            }
            else
            {
             /// if signout successful app will request to login first
              await Navigation.PushModalAsync(new Login());
            }
        }
    }
}
