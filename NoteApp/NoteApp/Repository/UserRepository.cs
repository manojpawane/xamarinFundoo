namespace NoteApp.Repository
{
    using Firebase.Database;
    using Firebase.Database.Query;
    using NoteApp.Interfaces;
    using NoteApp.Models;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Database layer defination
    /// </summary>
    public class UserRepository
    {
        /// <summary>
        /// The firebase client
        /// </summary>
        FirebaseClient firebaseClient = new FirebaseClient("https://todonotes-b0960.firebaseio.com/");

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task AddUserAsync(string firstName, string lastName, string email, string password)
        {
            /// Calls run time service from respective device (Android, iOS, UWP) to send request to add new user
            string uid = await DependencyService.Get<IFirebaseAuthenticator>().AddUserWithEmailPassword(email, password);

            /// To add user details in database
           await firebaseClient.Child("User").Child(uid).Child("UserInfo").PostAsync<User>(new User() { Uid = uid, FirstName = firstName, LastName = lastName });
        }
    }
}
