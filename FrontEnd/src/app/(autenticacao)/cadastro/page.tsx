"use client";

import userService from "@/services/users";
import Link from "next/link";
import { FormEvent, useState } from "react";
import { useRouter } from "next/navigation";
import { treatErrorAxios } from "@/helpers/treat-error-axios";
import Loading from "@/components/loading";

export default function Cadastro() {
  const router = useRouter();

  const [user, setUser] = useState({
    name: "",
    email: "",
    pass: "",
    confirmPass: "",
  });

  const [isLoading, setIsLoading] = useState(false);

  const handleUserForm = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (user.pass !== user.confirmPass) {
      alert("As senhas não coincidem.");
      return;
    }

    setIsLoading(true);

    try {
      await userService.createUser({
        name: user.name,
        email: user.email,
        password: user.pass,
      });

      alert("Usuário criado com sucesso");
      router.push("/");
    } catch (err: unknown) {
      const message = treatErrorAxios(err);

      alert(message);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <main className="flex items-center justify-center h-full bg-[rgb(240,242,247)]">
      {isLoading && <Loading />}
      <div className="bg-white p-8 rounded-lg shadow-lg w-full max-w-sm border-4 border-gray-300">
        <h2 className="text-2xl font-semibold text-center mb-6">Cadastro</h2>

        <form onSubmit={handleUserForm}>
          <div className="mb-4">
            <label
              htmlFor="name"
              className="block text-sm font-medium text-gray-700"
            >
              Nome
            </label>
            <input
              type="text"
              id="name"
              name="name"
              value={user.name}
              onChange={(e) => setUser({ ...user, name: e.target.value })}
              required
              className="w-full px-4 py-2 mt-1 border border-gray-300 rounded-md"
              placeholder="Digite seu nome"
            />
          </div>

          <div className="mb-4">
            <label
              htmlFor="email"
              className="block text-sm font-medium text-gray-700"
            >
              Email
            </label>
            <input
              type="email"
              id="email"
              name="email"
              value={user.email}
              onChange={(e) => setUser({ ...user, email: e.target.value })}
              required
              className="w-full px-4 py-2 mt-1 border border-gray-300 rounded-md"
              placeholder="Digite seu email"
            />
          </div>

          <div className="mb-4">
            <label
              htmlFor="password"
              className="block text-sm font-medium text-gray-700"
            >
              Senha
            </label>
            <input
              type="password"
              id="password"
              name="password"
              value={user.pass}
              onChange={(e) => setUser({ ...user, pass: e.target.value })}
              required
              className="w-full px-4 py-2 mt-1 border border-gray-300 rounded-md"
              placeholder="Digite sua senha"
            />
          </div>

          <div className="mb-6">
            <label
              htmlFor="confirmPassword"
              className="block text-sm font-medium text-gray-700"
            >
              Confirmar Senha
            </label>
            <input
              type="password"
              id="confirmPassword"
              name="confirmPassword"
              value={user.confirmPass}
              onChange={(e) =>
                setUser({ ...user, confirmPass: e.target.value })
              }
              required
              className="w-full px-4 py-2 mt-1 border border-gray-300 rounded-md"
              placeholder="Confirme sua senha"
            />
          </div>

          <button
            type="submit"
            className="w-full py-2 px-4 bg-indigo-500 text-white font-semibold rounded-md hover:bg-indigo-600"
          >
            Cadastrar
          </button>
        </form>

        <div className="mt-4 text-center">
          <Link href="/" className="text-sm text-indigo-500 hover:underline">
            Já tem uma conta? Faça login
          </Link>
        </div>
      </div>
    </main>
  );
}
