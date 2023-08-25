import * as Oidc from "oidc-client";

export const login = async () => {
  const userManager = new Oidc.UserManager({
    authority: "https://idsrv.marcelbenders.com",
    client_id: "js",
    redirect_uri: "https://localhost:5003/callback.html",
    response_type: "code",
    scope: "openid profile pbi_read",
    post_logout_redirect_uri: "https://localhost:5003/index.html",
  });
  try {
    await userManager.signinRedirect();
  } catch (e) {
    console.error(e);
  }
}