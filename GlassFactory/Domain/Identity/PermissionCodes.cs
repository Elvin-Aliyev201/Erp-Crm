using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public static class PermissionCodes
    {
        public const string UsersRead = "users.read";
        public const string UserManage = "user.manage";

        public const string RecipeApprove = "recipe.approve";

        public const string LabSampleApprove = "labsample.approve";

        public const string ProductionExecute = "production.execute";



        public static IReadOnlyDictionary<string, string> All { get; }=new Dictionary<string, string>
        {
            {UsersRead,"Users Read"},
            {UserManage,"User Manage"},
            {RecipeApprove,"Recipe Approve"},
            {LabSampleApprove,"Lab Sample Approve"},
            {ProductionExecute,"Production Execute"}
        };

    }
}
