import * as Oidc from "oidc-client";
import { UserManager } from "oidc-client";

export const hasUser = async () => {
  const userManager = new UserManager({
    authority: "http://localhost:5000",
    client_id: "pbi.admin",
    redirect_uri: "http://localhost:9000/signin-oidc",
    response_type: "code",
    scope: "openid profile pbi_admin",
    post_logout_redirect_uri: "http://localhost:9000/signout-callback-oidc",
  });
  const user = await userManager.getUser();
  if (user)
    if (user?.expired)
      return false;
    else
      return true;
  return false;
}
export const getUser = async () => {
  const userManager = new UserManager({
    authority: "http://localhost:5000",
    client_id: "pbi.admin",
    redirect_uri: "http://localhost:9000/signin-oidc",
    response_type: "code",
    scope: "openid profile pbi_admin",
    post_logout_redirect_uri: "http://localhost:9000/signout-callback-oidc",

  });
  const user = await userManager.getUser();
  console.log(user?.scope);
  console.log(user?.access_token);
  console.log(JSON.stringify(user));
}
export const red = () => {
  new Oidc.UserManager({response_mode: "query"}).signinRedirectCallback().then(function () {
    window.location.href = "/";
  }).catch(function (e) {
    console.error(e);
  });
}
export const login = async () => {
  const userManager = new Oidc.UserManager({
    // authority: "https://idsrv.marcelbenders.com",
    authority: "http://localhost:5000",
    client_id: "pbi.admin",
    redirect_uri: "http://localhost:9000/signin-oidc",
    response_type: "code",
    scope: "openid profile pbi_admin",
    post_logout_redirect_uri: "http://localhost:9000/signout-callback-oidc",
  });
  try {
    await userManager.signinRedirect();
  } catch (e) {
    console.error(e);
  }
}