using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.BusinessLayer.IBusiness
{
    public interface INoteBL
    {
        Response Insert(NoteModel note);
        Response Update(NoteModel note);
        NoteIndexModel GetListByRelationIdAndType(NoteIndexModel indexModel);
        NoteModel GetByIdAndType(int id, int type);
        IEnumerable<NoteModel> GetListByRelationIdAndTypeNoCount(int leadsId, int type);
    }
}
