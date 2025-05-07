export default function Home() {
  return (
    <main className="p-8 max-w-3xl mx-auto">
      <h1 className="text-3xl font-bold mb-8 text-center">Carteira Digital</h1>

      <section className="mb-8">
        <h2 className="text-xl font-semibold mb-2">Saldo da Carteira</h2>
        <div className="bg-white border rounded-md p-4 shadow">
          <p className="text-2xl font-bold text-green-600">R$ 0,00</p>
        </div>
      </section>

      <section className="mb-8">
        <h2 className="text-xl font-semibold mb-2">Adicionar Saldo</h2>
        <div className="flex gap-4 items-center">
          <input
            type="number"
            placeholder="Valor"
            className="w-full px-4 py-2 border rounded-md"
          />
          <button className="bg-indigo-600 cursor-pointer text-white px-4 py-2 rounded-md hover:bg-indigo-700 transition-colors">
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
            className="w-full px-4 py-2 border rounded-md"
          />
          <input
            type="number"
            placeholder="Valor da transferência"
            className="w-full px-4 py-2 border rounded-md"
          />
          <button className="bg-purple-600 cursor-pointer text-white px-4 py-2 rounded-md hover:bg-purple-700 transition-colors w-full">
            Transferir
          </button>
        </div>
      </section>

      <section>
        <h2 className="text-xl font-semibold mb-2">
          Histórico de Transferências
        </h2>

        <div className="flex items-center gap-4 mb-4 flex-wrap">
          <input type="date" className="px-4 py-2 border rounded-md" />
          <input type="date" className="px-4 py-2 border rounded-md" />
          <button className="bg-gray-200 px-4 py-2 cursor-pointer rounded-md hover:bg-gray-300">
            Filtrar
          </button>
        </div>

        <div className="overflow-x-auto">
          <table className="min-w-full bg-white border border-gray-200 rounded-md">
            <thead>
              <tr className="bg-gray-100 text-gray-700">
                <th className="text-left px-4 py-2 border-b">Destinatário</th>
                <th className="text-left px-4 py-2 border-b">Valor</th>
                <th className="text-left px-4 py-2 border-b">Data</th>
              </tr>
            </thead>
            <tbody>
              <tr className="hover:bg-gray-50">
                <td className="px-4 py-2 border-b">Usuário Exemplo</td>
                <td className="px-4 py-2 border-b">R$ 100,00</td>
                <td className="px-4 py-2 border-b">2025-05-01</td>
              </tr>
              <tr className="hover:bg-gray-50">
                <td className="px-4 py-2 border-b">Maria Silva</td>
                <td className="px-4 py-2 border-b">R$ 250,00</td>
                <td className="px-4 py-2 border-b">2025-05-03</td>
              </tr>
            </tbody>
          </table>
        </div>
      </section>
    </main>
  );
}
