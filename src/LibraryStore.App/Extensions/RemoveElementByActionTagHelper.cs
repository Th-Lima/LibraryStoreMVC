using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LibraryStore.App.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-action")]
    public class RemoveElementByActionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }

        public RemoveElementByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

            if (ActionName.Contains(action))
                return;

            output.SuppressOutput();
        }
    }
}
