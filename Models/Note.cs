using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wizard.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public int RequestId { get; set; }
        public string NoteType { get; set; }
        public string Content { get; set; }
        public string[] NoteFiles { get; set; }
    }
}
