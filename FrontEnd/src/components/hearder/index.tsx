"use client";

import { signOut, useSession } from "next-auth/react";

export const Header = () => {
  const { data: session } = useSession();

  return (
    <header className="bg-[linear-gradient(90deg,_rgb(255,1,150)_0%,_rgb(124,51,185)_100%)] text-white flex items-center justify-between p-4">
      <h1 className="text-xl font-bold">Carteira</h1>
      <section className="flex gap-5 items-center">
        <p className={`text-lg ${session ? "" : "italic"}`}>
          {session
            ? `Bem-vindo, ${session.user?.name} !`
            : "Transferências mais rápidas que o Pix!"}
        </p>
        {session && (
          <button
            onClick={() => signOut({ callbackUrl: "/" })}
            className="text-lg cursor-pointer font-medium hover:text-gray-200"
          >
            Sair
          </button>
        )}
      </section>
    </header>
  );
};
