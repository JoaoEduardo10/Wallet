"use client";

import { useSession } from "next-auth/react";
import { useEffect, useState } from "react";
import { Wallet } from "../models/wallet";
import walletService from "@/services/wallet";
import IGenericSession from "../models/Dtos/session";
import { treatErrorAxios } from "@/helpers/treat-error-axios";
import transactionService from "@/services/transaction";
import { FilterTransaction } from "../models/Dtos/filterTransaction";
import { TransactionDto } from "../models/Dtos/transactionDto";
import Loading from "@/components/loading";

export default function Home() {
  const { data } = useSession();
  const session = data as IGenericSession;

  const [wallet, setWallet] = useState<Wallet>({
    balance: 0,
  } as Wallet);

  const [paginationRecipient, setPaginationRecipient] = useState({
    pageSize: 5,
    page: 1,
    totalItems: 0,
    totalPages: 0,
  });

  const [paginationSent, setPaginationSent] = useState({
    pageSize: 5,
    page: 1,
    totalItems: 0,
    totalPages: 0,
  });

  const [recipientTransactions, setRecipientTransactions] = useState<
    TransactionDto[]
  >([]);

  const [sentTransactions, setSentTransactions] = useState<TransactionDto[]>(
    []
  );

  const [filterRecipient, setFilterRecipient] = useState<FilterTransaction>(
    new FilterTransaction()
  );

  const [filterSent, setFilterSent] = useState<FilterTransaction>(
    new FilterTransaction()
  );

  const [newValue, setAddNewValue] = useState(0);
  const [transfer, setTransfer] = useState({
    receiverEmail: "",
    amount: 0,
  });

  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    setIsLoading(true);
    const request = async () => {
      try {
        if (session?.acessToken && session?.user?.id) {
          const walletResponse = await walletService.getWallet(
            session.user.id,
            session.acessToken
          );

          setWallet(walletResponse.data);

          const filter = new FilterTransaction();

          filter.walletId = walletResponse.data.id;

          const [transactionsRecipentResponse, transactionsSentResponse] =
            await Promise.all([
              transactionService.getAllRecipientTransactions(
                filter,
                session.acessToken
              ),

              transactionService.getAllSentTransactions(
                filter,
                session.acessToken
              ),
            ]);

          setRecipientTransactions(transactionsRecipentResponse.data.itens);
          setSentTransactions(transactionsSentResponse.data.itens);

          setPaginationRecipient({
            page: transactionsRecipentResponse.data.page,
            pageSize: transactionsRecipentResponse.data.pageSize,
            totalItems: transactionsRecipentResponse.data.totalItems,
            totalPages: transactionsRecipentResponse.data.totalPage,
          });

          setPaginationSent({
            page: transactionsSentResponse.data.page,
            pageSize: transactionsSentResponse.data.pageSize,
            totalItems: transactionsSentResponse.data.totalItems,
            totalPages: transactionsSentResponse.data.totalPage,
          });

          setFilterRecipient((f) => ({
            ...f,
            walletId: walletResponse.data.id,
          }));

          setFilterSent((f) => ({
            ...f,
            walletId: walletResponse.data.id,
          }));
        }
      } catch (error: unknown) {
        const message = treatErrorAxios(error);
        alert(message);
      } finally {
        setIsLoading(false);
      }
    };

    request();
  }, [session?.user, session?.acessToken]);

  const handlePageChangeRecipient = async (page: number) => {
    setIsLoading(true);

    try {
      const newFilter = {
        ...filterRecipient,
        pageSize: paginationRecipient.pageSize,
        page: page,
      };

      const transactionsResponse =
        await transactionService.getAllRecipientTransactions(
          newFilter,
          session.acessToken
        );

      setRecipientTransactions(transactionsResponse.data.itens);

      setPaginationRecipient({
        page: transactionsResponse.data.page,
        pageSize: transactionsResponse.data.pageSize,
        totalItems: transactionsResponse.data.totalItems,
        totalPages: transactionsResponse.data.totalPage,
      });
    } catch (err: unknown) {
      const message = treatErrorAxios(err);

      alert(message);
    } finally {
      setIsLoading(false);
    }
  };

  const handlePageChangeSent = async (page: number) => {
    setIsLoading(true);
    try {
      const filter = {
        ...filterSent,
        page: page,
      };

      const transactionsResponse =
        await transactionService.getAllSentTransactions(
          filter,
          session.acessToken
        );

      setSentTransactions(transactionsResponse.data.itens);

      setPaginationSent({
        page: transactionsResponse.data.page,
        pageSize: transactionsResponse.data.pageSize,
        totalItems: transactionsResponse.data.totalItems,
        totalPages: transactionsResponse.data.totalPage,
      });
    } catch (err: unknown) {
      const message = treatErrorAxios(err);

      alert(message);
    } finally {
      setIsLoading(false);
    }
  };

  const handleAddBalence = async () => {
    setIsLoading(true);

    try {
      await walletService.addBalance(wallet.id, newValue, session.acessToken);

      setWallet((w) => ({ ...w, balance: wallet.balance + newValue }));

      setAddNewValue(0);
    } catch (err: unknown) {
      const message = treatErrorAxios(err);

      alert(message);
    } finally {
      setIsLoading(false);
    }
  };

  const hanlderTrensferValue = async () => {
    setIsLoading(true);

    try {
      await walletService.transferAmount(
        {
          amount: transfer.amount,
          receiverEmail: transfer.receiverEmail,
          senderWalletId: wallet.id,
        },
        session.acessToken
      );

      await Promise.all([
        handlePageChangeRecipient(filterRecipient.page),
        handlePageChangeSent(filterSent.page),
      ]);

      setWallet((w) => ({ ...w, balance: wallet.balance - transfer.amount }));

      setTransfer({
        amount: 0,
        receiverEmail: "",
      });
    } catch (err: unknown) {
      const message = treatErrorAxios(err);

      alert(message);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <main className="p-8 max-w-3xl mx-auto">
      {isLoading && <Loading />}
      <h1 className="text-3xl font-bold mb-8 text-center">Carteira Digital</h1>

      <section className="mb-8">
        <h2 className="text-xl font-semibold mb-2">Saldo da Carteira</h2>
        <div className="bg-white border rounded-md p-4 shadow">
          <p className="text-2xl font-bold text-green-600">
            {wallet.balance.toFixed(2)} R$
          </p>
        </div>
      </section>

      <section className="mb-8">
        <h2 className="text-xl font-semibold mb-2">Adicionar Saldo</h2>
        <div className="flex gap-4 items-center">
          <input
            type="number"
            onChange={(e) => setAddNewValue(+e.target.value)}
            value={newValue}
            placeholder="Valor"
            className="w-full px-4 py-2 border rounded-md"
          />
          <button
            onClick={handleAddBalence}
            className="bg-indigo-600 cursor-pointer text-white px-4 py-2 rounded-md hover:bg-indigo-700 transition-colors"
          >
            Adicionar
          </button>
        </div>
      </section>

      <section className="mb-8">
        <h2 className="text-xl font-semibold mb-2">Criar Transferência</h2>
        <div className="space-y-4">
          <input
            type="text"
            placeholder="Destinatário (e-mail)"
            onChange={(e) =>
              setTransfer((t) => ({ ...t, receiverEmail: e.target.value }))
            }
            value={transfer.receiverEmail}
            className="w-full px-4 py-2 border rounded-md"
          />
          <input
            type="number"
            onChange={(e) =>
              setTransfer((t) => ({ ...t, amount: +e.target.value }))
            }
            value={transfer.amount}
            placeholder="Valor da transferência"
            className="w-full px-4 py-2 border rounded-md"
          />
          <button
            onClick={hanlderTrensferValue}
            className="bg-purple-600 cursor-pointer text-white px-4 py-2 rounded-md hover:bg-purple-700 transition-colors w-full"
          >
            Transferir
          </button>
        </div>
      </section>

      <section>
        <h2 className="text-xl mb-7 text-center font-semibold">
          Histórico de Transferências
        </h2>

        <div className="overflow-x-auto">
          <h3 className="text-xl font-semibold mb-2">Recebidos </h3>

          <table className="min-w-full bg-white border border-gray-200 rounded-md">
            <thead>
              <tr className="bg-gray-100 text-gray-700">
                <th className="text-left px-4 py-2 border-b">Responsavel</th>
                <th className="text-left px-4 py-2 border-b">Valor</th>
                <th className="text-left px-4 py-2 border-b">Data</th>
              </tr>
            </thead>
            <tbody>
              {recipientTransactions.length > 0 ? (
                recipientTransactions.map((transaction, index) => (
                  <tr key={index} className="hover:bg-gray-50">
                    <td className="px-4 py-2 border-b">
                      {transaction.recipient}
                    </td>
                    <td className="px-4 py-2 border-b">
                      R$ {transaction.amount.toFixed(2)}
                    </td>
                    <td className="px-4 py-2 border-b">
                      {new Date(transaction.data).toLocaleDateString()}
                    </td>
                  </tr>
                ))
              ) : (
                <tr className="hover:bg-gray-50">
                  <td colSpan={3} className="px-4 text-center py-2 border-b">
                    Sem transações
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>

        <div className="flex justify-center gap-2 mt-4">
          <button
            onClick={() =>
              handlePageChangeRecipient(paginationRecipient.page - 1)
            }
            disabled={paginationRecipient.page === 1}
            className="bg-gray-200 cursor-pointer disabled:cursor-no-drop text-gray-700 px-4 py-2 rounded-md disabled:opacity-50"
          >
            Anterior
          </button>
          <span className="flex items-center">
            Página {paginationRecipient.page} de{" "}
            {paginationRecipient.totalPages}
          </span>
          <button
            onClick={() =>
              handlePageChangeRecipient(paginationRecipient.page + 1)
            }
            disabled={
              paginationRecipient.page === paginationRecipient.totalPages
            }
            className="bg-gray-200 cursor-pointer disabled:cursor-no-drop text-gray-700 px-4 py-2 rounded-md disabled:opacity-50"
          >
            Próxima
          </button>
        </div>

        <div className="overflow-x-auto mt-7">
          <h3 className="text-xl font-semibold mb-2">Enviados </h3>

          <table className="min-w-full bg-white border border-gray-200 rounded-md">
            <thead>
              <tr className="bg-gray-100 text-gray-700">
                <th className="text-left px-4 py-2 border-b">Destinatário</th>
                <th className="text-left px-4 py-2 border-b">Valor</th>
                <th className="text-left px-4 py-2 border-b">Data</th>
              </tr>
            </thead>
            <tbody>
              {sentTransactions.length >= 0 ? (
                sentTransactions.map((transaction, index) => (
                  <tr key={index} className="hover:bg-gray-50">
                    <td className="px-4 py-2 border-b">
                      {transaction.recipient}
                    </td>
                    <td className="px-4 py-2 border-b">
                      R$ {transaction.amount.toFixed(2)}
                    </td>
                    <td className="px-4 py-2 border-b">
                      {new Date(transaction.data).toLocaleDateString()}
                    </td>
                  </tr>
                ))
              ) : (
                <tr className="hover:bg-gray-50">
                  <td colSpan={3} className="px-4 text-center py-2 border-b">
                    Sem transações
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>

        <div className="flex justify-center gap-2 mt-4">
          <button
            onClick={() => handlePageChangeSent(paginationSent.page - 1)}
            disabled={paginationSent.page === 1}
            className="bg-gray-200 text-gray-700 cursor-pointer disabled:cursor-no-drop px-4 py-2 rounded-md disabled:opacity-50"
          >
            Anterior
          </button>
          <span className="flex items-center">
            Página {paginationSent.page} de {paginationSent.totalPages}
          </span>
          <button
            onClick={() => handlePageChangeSent(paginationSent.page + 1)}
            disabled={paginationSent.page === paginationSent.totalPages}
            className="bg-gray-200 cursor-pointer disabled:cursor-no-drop text-gray-700 px-4 py-2 rounded-md disabled:opacity-50"
          >
            Próxima
          </button>
        </div>
      </section>
    </main>
  );
}
