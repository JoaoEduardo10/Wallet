import api from "./axios";

class WalletService {
  private readonly url = "Wallet";

  getWallet(userId: string, token: string) {
    return api.get(`${this.url}/${userId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
  }

  transferAmount(
    transferData: {
      senderWalletId: string;
      receiverEmail: string;
      amount: number;
    },
    token: string
  ) {
    return api.post(`${this.url}/transfer`, transferData, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
  }

  addBalance(id: string, amount: number, token: string) {
    return api.put(`${this.url}/add-balance/${id}`, amount, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
  }
}

const walletService = new WalletService();

export default walletService;
