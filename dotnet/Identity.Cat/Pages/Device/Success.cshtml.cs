using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Cat.Pages.Device;

[SecurityHeaders]
[Authorize]
public class SuccessModel : PageModel
{
  public void OnGet()
  {
  }
}
