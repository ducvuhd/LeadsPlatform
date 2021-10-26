using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace APVN.LeadsPlatform.DataAccessLayer
{
    public class NoteDAL : INoteDAL
    {
        public int Insert(Note note)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                DynamicParameters dynamicParams = new DynamicParameters();
                dynamicParams.Add("RelationId", note.RelationId);
                dynamicParams.Add("Type", note.Type);
                dynamicParams.Add("CurrentRelationStatus", note.CurrentRelationStatus);
                dynamicParams.Add("Notes", note.Notes);
                dynamicParams.Add("CreatedDate", note.CreatedDate);
                dynamicParams.Add("CreatedBy", note.CreatedBy);
                dynamicParams.Add("LastModifiedDate", note.LastModifiedDate);
                dynamicParams.Add("LastModifiedBy", note.LastModifiedBy);
                dynamicParams.Add("CurrentInActive", note.CurrentInActive);
                return connection.QuerySingle<int>("Note_Insert_V1", dynamicParams, commandType: CommandType.StoredProcedure);
            }
        }

        public void Update(Note note)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                DynamicParameters dynamicParams = new DynamicParameters();
                dynamicParams.Add("Id", note.Id);
                dynamicParams.Add("RelationId", note.RelationId);
                dynamicParams.Add("Type", note.Type);
                dynamicParams.Add("CurrentRelationStatus", note.CurrentRelationStatus);
                dynamicParams.Add("Notes", note.Notes);
                dynamicParams.Add("CreatedDate", note.CreatedDate);
                dynamicParams.Add("CreatedBy", note.CreatedBy);
                dynamicParams.Add("LastModifiedDate", note.LastModifiedDate);
                dynamicParams.Add("LastModifiedBy", note.LastModifiedBy);
                dynamicParams.Add("CurrentInActive", note.CurrentInActive);
                connection.Execute("Note_Update_V1", dynamicParams, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<NoteModel> GetListByRelationIdAndType(NoteIndexModel indexModel, out int totalRecord)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("RelationId", indexModel.RelationId);
                dynamicParams.Add("Type", indexModel.Type);
                dynamicParams.Add("PageIndex", indexModel.PageIndex);
                dynamicParams.Add("PageSize", indexModel.PageSize);
                dynamicParams.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var lst = connection.Query<NoteModel>("Note_GetListByRelationIdAndType", dynamicParams, commandType: CommandType.StoredProcedure);
                totalRecord = dynamicParams.Get<int>("TotalRecord");
                return lst;
            }
        }
        public IEnumerable<NoteModel> GetListByRelationIdAndTypeNoCount(int leadsId, int type)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("RelationId", leadsId);
                dynamicParams.Add("Type", type);

                var lst = connection.Query<NoteModel>("Note_GetListByRelationIdAndTypeNoCount", dynamicParams, commandType: CommandType.StoredProcedure);
                return lst;
            }
        }
        public NoteModel GetByIdAndType(int id, int type)
        {
            using (var connection = new SqlConnection(AppSettings.Instance.GetConnection("DefaultConnection")))
            {
                return connection.QueryFirstOrDefault<NoteModel>("Note_GetByIdAndType", new
                {
                    Id = id,
                    Type = type
                }, commandType: CommandType.StoredProcedure); ;
            }
        }
    }
}
