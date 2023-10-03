using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace com.b_velop.IdentityCat.Service.Pages;

[AllowAnonymous]
[SecurityHeaders]
public class PrivacyModel : PageModel

{
    public void OnGet()
    {
    }
}