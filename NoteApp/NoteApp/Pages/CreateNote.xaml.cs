namespace NoteApp.Pages
{
    using Firebase.Database;
    using Firebase.Database.Query;
    using NoteApp.Interfaces;
    using NoteApp.Models;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using static NoteApp.Common.Enum;

    /// <summary>
    /// Create the note and stores in database
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateNote : ContentPage
	{
        /// <summary>
        /// The firebase client
        /// </summary>
        FirebaseClient firebaseClient = new FirebaseClient("https://todonotes-b0960.firebaseio.com/");

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNote"/> class.
        /// </summary>
        public CreateNote ()
		{
			InitializeComponent ();
		}

        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// </summary>
        /// <returns>
        /// To be added.
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override bool OnBackButtonPressed()
        {   
            /// It checks whether running device is android if it is Android it post the note in database
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                /// Get(s) the existing user which is current active
                var uid = DependencyService.Get<IFirebaseAuthenticator>().User();

                /// sends request to store data in database
                var response = firebaseClient.Child("User").Child(uid).Child("Note").PostAsync<Note>(new Note() { Title = title.Text, Content = note.Text, noteType = NoteType.isNote});
                return base.OnBackButtonPressed();
            }
            else
            {
                return false;
            }
        }
    }
}