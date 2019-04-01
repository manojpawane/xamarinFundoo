using Firebase.Database;
using Firebase.Database.Query;
using NoteApp.Interfaces;
using NoteApp.Models;
using NoteApp.Pages.Edit_PopUp;
using NoteApp.Repository;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static NoteApp.Common.Enum;

namespace NoteApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateNote : ContentPage
	{
        string noteKeys = "";
        NotesRepository notesRepository = new NotesRepository();
        FirebaseClient firebaseClient = new FirebaseClient("https://todonotes-b0960.firebaseio.com/");

        public UpdateNote (string noteKey)
		{
            noteKeys = noteKey;
            UpdateNotes();
            InitializeComponent();
		}

        
        public async void UpdateNotes()
        {
            string uid =  DependencyService.Get<IFirebaseAuthenticator>().User();
            Note note = await notesRepository.GetNoteByKeyAsync( noteKeys, uid);

            
            editor.Text = note.Title;
            editorContent.Text = note.Content;
            ToolbarItems.Clear();
            if(note.noteType == NoteType.isNote)
            {
                ToolbarItems.Add(archived);
              
            }
            else
            {
                ToolbarItems.Add(unarchived);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            /// It checks whether running device is android if it is Android it post the note in database
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                /// Get(s) the existing user which is current active
                var uid = DependencyService.Get<IFirebaseAuthenticator>().User();
                
                Note newNote = new Note()
                {
                    Title = editor.Text,
                    Content = editorContent.Text
                };
                notesRepository.UpdateNoteAsync(newNote, noteKeys, uid);
                
                return base.OnBackButtonPressed();
            }
            else
            {
                return false;
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            string uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            Note note = await notesRepository.GetNoteByKeyAsync(noteKeys, uid);
            note.noteType = NoteType.isArchive;
            notesRepository.UpdateNoteAsync(note, noteKeys, uid);
            await Navigation.PushAsync(new NoteView());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new EditPopup(noteKeys));
        }
    }
}