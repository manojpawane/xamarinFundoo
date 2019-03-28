
using static NoteApp.Common.Enum;

namespace NoteApp.Models
{
    /// <summary>
    /// The Model class for Note
    /// </summary>
    public class Note
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        public NoteType noteType { get; set; }
    }
}
