namespace NoteApp.Repository
{
    using Firebase.Database;
    using Firebase.Database.Query;
    using NoteApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NotesRepository
    {
        /// <summary>
        /// Data base URL
        /// </summary>
        FirebaseClient firebaseClient = new FirebaseClient("https://todonotes-b0960.firebaseio.com/");

        public async Task<IList<Note>> GetNotesAsync(string uid)
        {
            /// Gets the Note of respective user
            IList<Note> noteData = (await firebaseClient.Child("User").Child(uid).Child("Note").OnceAsync<Note>()).Select(item => new Note
            {
                Title = item.Object.Title,
                Content = item.Object.Content,
                Key = item.Key,
                noteType = item.Object.noteType
            }).ToList();

            return noteData;
        }

        public async Task<Note> GetNoteByKeyAsync(string key, string uid)
        {
            Note note = await firebaseClient.Child("User").Child(uid).Child("Note").Child(key).OnceSingleAsync<Note>();
            return note;
        }

        public async Task UpdateNoteAsync(Note note, string key, string uid)
        {
          await firebaseClient.Child("User").Child(uid).Child("Note").Child(key).PutAsync<Note>(new Note() { Title = note.Title, Content = note.Content, noteType = note.noteType});
        }
    }
}
