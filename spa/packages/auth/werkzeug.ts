import { WebStorageStateStore } from "oidc-client-ts";
import { Manager } from "./manager";

export const userStore = () =>
  new WebStorageStateStore({
    prefix: 'fuck-you:',
    store: window.localStorage,
  });

export const getAccessToken = async () => {
  const user = await getUser();
  return user?.access_token;
};

export const authority = () => "https://idsrv.marcelbenders.com";
// authority: 'https://localhost:5000',

export const host = () => "https://marcelbenders.com"
//"http://localhost:8067";

export const userLoggedIn = async () => {
  const user = await getUser();
  return user && !user.expired;
}

export const getUser = async () => {
  console.info('==== getUser');
  const userManager = Manager.userManager();
  const user = await userManager.getUser();
  console.info('==== (User)', user);
  return user;
};
