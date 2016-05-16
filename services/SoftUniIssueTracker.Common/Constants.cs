using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIT.Web
{
    public static class Constants
    {
        public const string SpecialFilterLabelPrefix = "!label!";

        public const string DefaultSchemeName = "default scheme";
        public const string UnexistingStatusErrorMessage = "A status with this id doesn't exist";
        public const string UnexistingIssueErrorMessage = "An issue with this id doesn't exist";
        public const string UnexistingProjectErrorMessage = "A project with this id doesn't exist";
        public const string UnexistingUserErrorMessage = "An user with this id doesn't exist";
        public const string UnexistingPriorityForProjectErrorMessage = "There doesn't exist a priority with this id for this project";
        public const string UnexistingTransitionSchemeErrorMessage = "A transition scheme with this id doesn't exist";
        public const string UnavailableStatusForIssue = "The status with this id isn't available for this issue";
        public const string NotProjectLeader = "Only the project leader can access this endpoint";
        public const string NotAdmin = "Only admins can access this endpoint";
        public const string NotProjectLeaderOrAdmin = "Only the project leader and admin can access this endpoint";
        public const string NotProjectLeaderOrAdminOrAssignee = "Only the project leader, the issue assignee and the admin can access this endpoint";
        public const string NotAssociatedWithIssue = "Not associated with issue (not project leader, neither has an assigned issue in this project";
        public const string AlreadyAdmin = "User is already admin";
        public const string UnexistingLabel = "This label does not exist";
    }
}
