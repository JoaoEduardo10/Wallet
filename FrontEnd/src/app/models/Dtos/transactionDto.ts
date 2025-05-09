export class TransactionDto {
  amount: number;
  data: string; // DateTime em C# vira string (ISO) em TS
  recipient: string;

  constructor(amount: number, data: string, recipient: string = "") {
    this.amount = amount;
    this.data = data;
    this.recipient = recipient;
  }
}
