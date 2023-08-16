using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Cata.Pages.Device;

[SecurityHeaders]
[Authorize]
public class SuccessModel : PageModel
{
  public void OnGet()
  {
  }
}
