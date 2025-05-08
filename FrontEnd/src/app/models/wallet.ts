import { Transaction } from "./transactions";
import { User } from "./user";

export class Wallet {
  id: string;
  user: User;
  userId: string;
  balance: number;
  createdAt: Date;
  updatedAt: Date;
  transactions: Transaction[];

  constructor() {
    this.id = "";
    this.user = new User();
    this.userId = "";
    this.balance = 0;
    this.createdAt = new Date();
    this.updatedAt = new Date();
    this.transactions = [];
  }
}
