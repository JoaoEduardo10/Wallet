import { TransactionStatusEnum } from "./enums/transactionStatus.enum";
import { Wallet } from "./wallet";

export class Transaction {
  id: string;
  senderWalletId: string;
  senderWallet: Wallet;
  receiverWalletId: string;
  receiverWallet: Wallet;
  amout: number;
  status: TransactionStatusEnum;
  createdAt: Date;

  constructor() {
    this.id = "";
    this.senderWalletId = "";
    this.senderWallet = new Wallet();
    this.receiverWalletId = "";
    this.receiverWallet = new Wallet();
    this.amout = 0;
    this.status = TransactionStatusEnum.Pending;
    this.createdAt = new Date();
  }
}
