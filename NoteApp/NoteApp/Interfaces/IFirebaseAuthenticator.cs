namespace NoteApp.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for the firebase authentication
    /// </summary>
    public interface IFirebaseAuthenticator
    {
        /// <summary>
        /// Logins the with email password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<string> LoginWithEmailPassword(string email, string password);

        /// <summary>
        /// Adds the user with email password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<string> AddUserWithEmailPassword(string email, string password);

        /// <summary>
        /// Sends the reset password mail.
        /// </summary>
        /// <param name="email">The email.</param>
        void SendResetPasswordMail(string email);

        /// <summary>
        /// Represents an event that is raised when the sign-out operation is complete.
        /// </summary>
        /// <returns></returns>
        string SignOut();

        /// <summary>
        /// Users this instance.
        /// </summary>
        /// <returns></returns>
        string User();
    }
}
