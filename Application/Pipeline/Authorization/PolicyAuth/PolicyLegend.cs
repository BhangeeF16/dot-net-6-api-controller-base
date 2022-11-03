﻿namespace Application.Pipeline.Authorization.PolicyAuth
{
    public class PolicyLegend
    {
        #region Admin Only Policies

        public const string AdminOnly = "AdminOnly";
        public const string CandidateOnly = "CandidateOnly";
        public const string CorporateOnly = "CorporateOnly";

        #endregion

        #region User Permissions

        public const string CanAddUser = "CanAddUser";
        public const string CanEditUser = "CanEditUser";
        public const string CanDeleteUser = "CanDeleteUser";
        public const string CanViewUsers = "CanViewUsers";

        #endregion
    }
}