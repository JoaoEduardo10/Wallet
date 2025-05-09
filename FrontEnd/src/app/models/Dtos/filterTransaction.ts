export class FilterTransaction {
  walletId: string;
  page: number;
  pageSize: number;
  startDate?: string;
  endDate?: string;

  constructor() {
    this.walletId = "";
    this.page = 1;
    this.pageSize = 5;
    this.startDate = "";
    this.endDate = "";
  }
}
