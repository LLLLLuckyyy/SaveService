using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaveService.Validation
{
    public static class DateValidation
    {

        public static bool IsAllowedToChange(DateTime createdAt)
        {
            return (DateTime.Now - createdAt).TotalMinutes > 15;
        }
    }
}
