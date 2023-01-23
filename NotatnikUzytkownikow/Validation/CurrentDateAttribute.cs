﻿using System.ComponentModel.DataAnnotations;

namespace NotatnikUzytkownikow.Validation
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime <= DateTime.Now;
        }
    }
}
