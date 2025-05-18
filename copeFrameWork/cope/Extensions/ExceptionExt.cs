#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace cope.Extensions
{
    public static class ExceptionExt
    {
        public static string[] GetInfo(this Exception ex)
        {
            List<string> lines = new List<string>();
            lines.Add("Exception Info");
            lines.Add("Type: " + ex.GetType().FullName);
            lines.Add("Message: " + ex.Message);
            lines.Add("Source: " + ex.Source);
            lines.Add("Target site: " + ex.TargetSite);
            lines.Add("Stack trace: ");
            lines.AddRange(ex.StackTrace.Split(StringSplitOptions.RemoveEmptyEntries, '\n'));
            lines.Add(string.Empty);
            lines.Add("Additional data: ");
            if (ex.Data.Count <= 0)
                lines.Add("No additional data available.");
            else
                lines.AddRange(from DictionaryEntry de in ex.Data select de.Key + ": " + de.Value);

            if (ex.InnerException == null)
                lines.Add("No inner exception.");
            else
            {
                lines.Add("Inner exception following:");
                lines.AddRange(GetInfo(ex.InnerException).MapInplace(s => "    " + s));
            }
            return lines.ToArray();
        }
    }
}