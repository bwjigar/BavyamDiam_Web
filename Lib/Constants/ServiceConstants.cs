using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Constants
{
    public static class ServiceConstants
    {
        public const string OwinChallengeFlag = "X-Challenge";

        public const string ProfilePhotoPath = "~/UserProfileImages/";
        public const string SessionUserID = "User_ID";
        public const string SessionUserName = "User_Name";
        public const string SessionTransID = "TransID";
        public const string SessionIpAddress = "Ip_Address";
        public const string SessionDeviseType = "Devise_Type";
        public const string IsAdmin = "IsAdmin";
        public const string IsEmp = "IsEmp";
        public const string IsGuest = "IsGuest";
    }

    public enum TransactionType
    {
        Add = 'A',
        Remove = 'R'
    }
}
