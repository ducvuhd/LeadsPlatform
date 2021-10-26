using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class NoteIndexModel : Pager
    {
        public int RelationId { get; set; }
        public int Type { get; set; }
        public List<NoteModel> ListNote { get; set; }
    }
}
