using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Helper
{
    public static class SlugHelper
    {
        public static  string GenerateSlug(string title)
        {
            var slug = Regex.Replace(title.ToLower(), @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", "-").Trim('-');
            return slug;
        }
    }
}
