export class FilterTransaction {
  walletId: string;
  page: number;
  pageSize: number;

  constructor() {
    this.walletId = "";
    this.page = 1;
    this.pageSize = 5;
  }
}
