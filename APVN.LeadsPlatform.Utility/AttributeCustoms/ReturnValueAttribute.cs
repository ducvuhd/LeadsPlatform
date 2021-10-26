using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace APVN.LeadsPlatform.Utility.AttributeCustoms
{
    public class ReturnValueFieldAttribute : Attribute
    {
        private string stringVal;

        public ReturnValueFieldAttribute()
        {
            stringVal = "";
        }

        public string Field
        {
            get { return stringVal; }
            set { stringVal = value; }
        }
    }
}
