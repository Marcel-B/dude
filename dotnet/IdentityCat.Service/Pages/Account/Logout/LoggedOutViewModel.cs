// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


namespace com.b_velop.IdentityCat.Service.Pages.Account.Logout;

public class LoggedOutViewModel
{
    public string PostLogoutRedirectUri { get; set; }
    public string ClientName { get; set; }
    public string SignOutIframeUrl { get; set; }
    public bool AutomaticRedirectAfterSignOut { get; set; }
}