using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class NoteBL : INoteBL
    {
        private readonly INoteDAL _noteDAL;

        public NoteBL(INoteDAL noteDAL)
        {
            _noteDAL = noteDAL;
        }

        public NoteIndexModel GetListByRelationIdAndType(NoteIndexModel indexModel)
        {
            int totalRecord = 0;
            var lst = _noteDAL.GetListByRelationIdAndType(indexModel, out totalRecord);
            indexModel.ListNote = lst.ToList();
            indexModel.TotalRecord = totalRecord;
            return indexModel;
        }

        public IEnumerable<NoteModel> GetListByRelationIdAndTypeNoCount(int leadsId, int type)
        {
            var lst = _noteDAL.GetListByRelationIdAndTypeNoCount(leadsId, type);
            return lst;
        }

        public Response Insert(NoteModel note)
        {
            try
            {
                int id = _noteDAL.Insert(note);
                note.Id = id;
                return new Response(SystemCode.Success, "Tạo thành công", note);
            }
            catch (Exception ex)
            {
                return new Response(SystemCode.Error, ex.Message, null);
            }
        }

        public Response Update(NoteModel note)
        {
            try
            {
                _noteDAL.Update(note);
                return new Response(SystemCode.Success, "Sửa thành công", note);
            }
            catch (Exception ex)
            {

                return new Response(SystemCode.Error, ex.Message, null);
            }
        }
        public NoteModel GetByIdAndType(int id, int type)
        {
            return _noteDAL.GetByIdAndType(id, type);
        }
    }
}
