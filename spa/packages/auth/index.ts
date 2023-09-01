import { UserManager } from "oidc-client";
import * as singleSpa from "single-spa";

const userManager = new UserManager({
  authority: "https://idsrv.marcelbenders.com",
  client_id: "pbi.admin",
  redirect_uri: "http://localhost:9000/signin-oidc",
  response_mode: "query",
  response_type: "code",
  checkSessionInterval: 30000, // Intervall in Millisekunden
  stopCheckSessionOnError: true,
  monitorSession: false,
  scope: "openid profile pbi_admin",
  post_logout_redirect_uri: "http://localhost:9000/signout-callback-oidc",
});

export const hasUser = async () => {
  console.info("==== hasUser");
  const user = await userManager.getUser();
  if (user)
    if (user?.expired)
      return false;
    else
      return true;
  return false;
}

export const getUser = async () => {
  console.info("==== getUser");
  const user = await userManager.getUser();
  console.info("==== (User)", user);
  return user;
}


export const getUsername = async () => {
  console.info("==== getUsername");
  const user = await getUser();
  return user?.profile?.name;
}

export const red = async () => {
  console.info("==== red");
  try {
    const user = await userManager.signinRedirectCallback();
    console.info("====User", user);
    document.location.href = '/';
  } catch (e) {
    const j = JSON.stringify(e);
    if (j.includes("login_required")) {
      console.info(j);
    }
  }
}
export const login = async () => {
  console.info("==== login");
  try {
    await userManager.signinRedirect();
  } catch (e) {
    console.error(e);
  }
}

export const getAccessToken = async () => {
  const user = await getUser();
  return user?.access_token;
}
