using NoteApp.Interfaces;
using NoteApp.Models;
using NoteApp.Repository;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static NoteApp.Common.Enum;

namespace NoteApp.Pages.Edit_PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditPopup : PopupPage
    {
        string noteKey = null;
        NotesRepository noteRepository = new NotesRepository();
		public EditPopup (string noteKey)
		{
            this.noteKey = noteKey;
			InitializeComponent ();
		}

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            Note note = new Note();
            var uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            note = await noteRepository.GetNoteByKeyAsync(this.noteKey, uid);
            note.noteType = NoteType.isTrash;
            noteRepository.UpdateNoteAsync(note, this.noteKey, uid);
            await PopupNavigation.Instance.PopAsync();
            await Navigation.PushAsync(new NoteView());
        }
    }
}