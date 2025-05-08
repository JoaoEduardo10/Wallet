export class Authentication {
  id: string;
  token: string;
  username: string;
  tokenExpirationTime: number;
  isAuthenticated: boolean;

  constructor() {
    this.id = "";
    this.token = "";
    this.username = "";
    this.tokenExpirationTime = 0;
    this.isAuthenticated = false;
  }
}
