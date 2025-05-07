export default function Login() {
  return (
    <main className="flex items-center justify-center h-full bg-gradient-to-r ">
      <div className="bg-white p-8 rounded-lg shadow-lg w-full max-w-sm border-4 border-gray-300">
        <h2 className="text-2xl font-semibold text-center mb-6">Login</h2>
        
        <form>
          <div className="mb-4">
            <label htmlFor="email" className="block text-sm font-medium text-gray-700">Email</label>
            <input
              type="email"
              id="email"
              name="email"
              required
              className="w-full px-4 py-2 mt-1 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
              placeholder="Digite seu email"
            />
          </div>

          <div className="mb-6">
            <label htmlFor="password" className="block text-sm font-medium text-gray-700">Senha</label>
            <input
              type="password"
              id="password"
              name="password"
              required
              className="w-full px-4 py-2 mt-1 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
              placeholder="Digite sua senha"
            />
          </div>

          <button
            type="submit"
            className="w-full py-2 px-4 bg-indigo-500 text-white font-semibold rounded-md hover:bg-indigo-600 focus:outline-none focus:ring-2 focus:ring-indigo-400"
          >
            Entrar
          </button>
        </form>
        
        <div className="mt-4 text-center">
            <a href="/cadastro" className="text-sm text-indigo-500 hover:underline">Não tem uma conta? Faça o cadastro</a>
          </div>
      </div>
    </main>
  );
}
