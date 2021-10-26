using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer
{
    public interface INoteDAL
    {
        int Insert(Note note);
        void Update(Note note);
        IEnumerable<NoteModel> GetListByRelationIdAndType(NoteIndexModel indexModel, out int totalRecord);
        NoteModel GetByIdAndType(int id, int type);
        IEnumerable<NoteModel> GetListByRelationIdAndTypeNoCount(int leadsId, int type);
    }
}
