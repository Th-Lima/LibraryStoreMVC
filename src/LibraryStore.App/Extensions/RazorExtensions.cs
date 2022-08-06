using Microsoft.AspNetCore.Mvc.Razor;

namespace LibraryStore.App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatDocument(this RazorPage page, int typePerson, string document)
        {
            return typePerson == 1 ? Convert.ToUInt64(document).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(document).ToString(@"00\.000\.000\/0000\-00");
        }
    }
}
