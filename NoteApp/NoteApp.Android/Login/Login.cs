using Xamarin.Forms;
using NoteApp.Droid.Login;

/// <summary>
/// The login class
/// </summary>
[assembly: Dependency(typeof(Login))]
namespace NoteApp.Droid.Login
{
    using System;
    using System.Threading.Tasks;
    using Firebase.Auth;
    using Firebase.Database;
    using NoteApp.Interfaces;

    /// <summary>
    /// The Login class with various methods
    /// </summary>
    /// <seealso cref="NoteApp.Interfaces.IFirebaseAuthenticator" />
    public class Login : IFirebaseAuthenticator
    {
        /// <summary>
        /// The firebase client
        /// </summary>
        FirebaseClient firebaseClient = new FirebaseClient("https://todonotes-b0960.firebaseio.com/");

        /// <summary>
        /// Logins the with email password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<string> LoginWithEmailPassword(string email, string password)
         {
            try
            {
                /// Login user with email and password as a parameter
                var result = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                return result.ToString();
            }
            catch(Exception)
            {
                return null;
            }
         }

        /// <summary>
        /// Users this instance.
        /// </summary>
        /// <returns></returns>
        public string User()
        {
            string userId = null;
            try
            {
                /// Gets the current user details which is login
                userId = FirebaseAuth.Instance.CurrentUser.Uid;
                return userId;
            }
            catch(Exception)
            {
                return userId;
            }
        }

        /// <summary>
        /// Adds the user with email password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<string> AddUserWithEmailPassword(string email, string password)
        {
            /// Adds the user with email and password
            var response = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
            return response.User.Uid;
        }

        /// <summary>
        /// Sends the reset password mail.
        /// </summary>
        /// <param name="email">The email.</param>
        public void SendResetPasswordMail(string email)
        {
            /// sends the reset password email to user on user request
            var response =  FirebaseAuth.Instance.SendPasswordResetEmail(email);
        }

        /// <summary>
        /// Represents an event that is raised when the sign-out operation is complete.
        /// </summary>
        /// <returns></returns>
        public string SignOut()
        {
            string user = null;
            try
            {
                /// Logout the user from current instance
                FirebaseAuth.Instance.SignOut();
                user = FirebaseAuth.Instance.CurrentUser.Uid;
                return user;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
