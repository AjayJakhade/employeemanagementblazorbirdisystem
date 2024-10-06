using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayerShared.Common
{
    public static class GenericMessage
    {
        public const string addSuccess = " Successfully added";
        public const string updateSuccess = " Successfully updated";
        public const string duplicateRecord = "Already exists";
        public const string saveErrorMessage = " Error occured while saving";
        public const string uploadError = " Error occured while uploading document";
        public const string uploadSuccess = " File uploded succesfully";
        public const string noRecordFound = " No record found";
        public const string badRequest = "Bad Request";
        public const string success = "alert-success";
        public const string error = "alert-danger";
    }
}
