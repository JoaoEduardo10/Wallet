import { FilterTransaction } from "@/app/models/Dtos/filterTransaction";
import api from "./axios";

class TransactionService {
  private readonly url = "transactions";

  getAllRecipientTransactions(filter: FilterTransaction, token: string) {
    return api.get(this.url + "/received", {
      params: filter,
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
  }

  getAllSentTransactions(filter: FilterTransaction, token: string) {
    return api.get(this.url + "/sent", {
      params: filter,
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
  }
}

const transactionService = new TransactionService();

export default transactionService;
