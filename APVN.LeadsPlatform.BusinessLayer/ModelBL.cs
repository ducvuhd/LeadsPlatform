using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class ModelBL : IModelBL
    {
        private IModelDAL _modelDAL;

        public ModelBL(IModelDAL modelDAL)
        {
            this._modelDAL = modelDAL;
        }

        public List<APVN.LeadsPlatform.Entity.Models> ListAll()
        {
            return (List<APVN.LeadsPlatform.Entity.Models>)_modelDAL.ListAll();
        }
    }
}
