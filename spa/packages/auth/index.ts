import { UserManager } from 'oidc-client-ts';

class Manager {
  static um: UserManager;
  static counter = 0;
  static got = 0;

  private constructor() {
    Manager.counter++;
  }

  public static userManager(): UserManager {
    this.got++;
    if (!this.um) {
      this.counter++;
      this.um = new UserManager({
        // authority: 'https://localhost:5000',
        authority: "https://idsrv.marcelbenders.com",
        client_id: 'pbi.admin',
        redirect_uri: 'http://localhost:9000/signin-oidc',
      });
    }
    console.info('==== Manager', Manager.counter, 'got', Manager.got);
    return this.um;
  }
}

export const hasUser = async () => {
  console.info('==== hasUser');
  const userManager = Manager.userManager();
  const user = await userManager.getUser();
  if (user)
    if (user?.expired) return false;
    else return true;
  return false;
};

export const getUser = async () => {
  console.info('==== getUser');
  const userManager = Manager.userManager();
  const user = await userManager.getUser();
  console.info('==== (User)', user);
  return user;
};

export const getUsername = async () => {
  console.info('==== getUsername');
  const user = await getUser();
  return user?.profile?.name;
};

export const getAccessToken = async () => {
  const user = await getUser();
  return user?.access_token;
};
