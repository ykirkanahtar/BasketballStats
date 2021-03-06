﻿using BasketballStats.Contracts.Enums;
using System.Collections.Generic;
using System.Linq;

namespace BasketballStats.WebSite.Utils
{
    public static class ConvertFunctions
    {
        public static string GetFirstCharOfEnumValues(IList<MatchResult> matchScores)
        {
            var returnValue = matchScores.Aggregate(string.Empty, (current, form) => current + $"{form.ToString().Substring(0, 1)}-");

            return returnValue.Remove(returnValue.Length - 1);
        }
    }
}
