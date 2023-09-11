import { UserManager } from "oidc-client-ts";
import { authority, userStore } from "./index";

export class Manager {
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
        authority: authority(),
        client_id: 'pbi.admin',
        redirect_uri: 'http://localhost:9000/signin-oidc',
        userStore: userStore()
      });
    }
    console.info('==== Manager', Manager.counter, 'got', Manager.got);
    return this.um;
  }
}