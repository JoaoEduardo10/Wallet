import { Wallet } from "./wallet";

export class User {
  id: string;
  name: string;
  email: string;
  password: string;
  wallet: Wallet;
  createdAt: Date;

  constructor() {
    this.id = "";
    this.name = "";
    this.email = "";
    this.password = "";
    this.wallet = new Wallet();
    this.createdAt = new Date();
  }
}
