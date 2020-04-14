using IsolatedDevelopment.Web.Dependencies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IsolatedDevelopment.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIntegratedDependency _dependency;

        public string Message;

        public IndexModel(IIntegratedDependency dependency)
        {
            _dependency = dependency;
        }

        public void OnGet()
        {
            Message = _dependency.GetMessage();
        }

        public IActionResult OnPost()
        {
            return Content(_dependency.GetMessage());
        }
    }
}
