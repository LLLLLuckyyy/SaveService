using System;

namespace SaveService.Common.Validation
{
    public static class DateValidation
    {
        public static bool IsEnoughTimeHasPassedToEditOrDelete(DateTime createdAt)
        {
            return (DateTime.Now - createdAt).TotalMinutes > 15;
        }
    }
}
