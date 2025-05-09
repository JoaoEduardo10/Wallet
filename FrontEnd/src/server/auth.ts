import { Authentication } from "@/app/models/auhtntication";
import { treatErrorAxios } from "@/helpers/treat-error-axios";
import UserService from "@/services/users";
import { DefaultSession, getServerSession, NextAuthOptions } from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";
import { Session } from "next-auth";
import { JWT } from "next-auth/jwt";
import IGenericSession from "@/app/models/Dtos/session";

export const authOptions: NextAuthOptions = {
  secret: process.env.NEXTAUTH_SECRET,
  session: {
    strategy: "jwt",
    maxAge: 3600,
  },
  pages: {
    signIn: "/",
    signOut: "/carteira",
  },
  providers: [
    CredentialsProvider({
      name: "credentials",
      credentials: {
        email: { label: "Email", type: "email", placeholder: "email" },
        password: { label: "Password", type: "password" },
      },

      async authorize(credentials) {
        try {
          const { email, password } = credentials!;

          const response = await UserService.login(email, password);

          return response.data;
        } catch (error: unknown) {
          const message = treatErrorAxios(error);

          throw new Error(message);
        }
      },
    }),
  ],
  callbacks: {
    jwt: async ({ token, user }) => {
      if (user) {
        const auth = user as Authentication;
        const isSignIN = auth.isAuthenticated;

        if (isSignIN) {
          token.name = auth.username;
          token.jwt = auth.token;
          token.expiration = auth.tokenExpirationTime;
          token.isAuthenticated = isSignIN;
          token.id = auth.id;
        }
      }

      const time = token.expiration as unknown as number;
      const actualDateInSeconds = Math.floor(Date.now() / 1000);

      if (!time || actualDateInSeconds > time) {
        return {}; // Retorna um objeto vazio caso o token tenha expirado
      }

      return token;
    },

    async session({
      session,
      token,
    }: {
      session: Session;
      token: JWT;
    }): Promise<Session | DefaultSession> {
      if (
        !token ||
        !token.jwt ||
        !session ||
        !token.isAuthenticated ||
        !token.id
      ) {
        return { ...session, user: {} };
      }

      const newSession = {
        ...session,
        acessToken: token.jwt,
        user: {
          isAuthenticated: false,
          ...session.user,
          id: token.id,
        },
      };

      newSession.user = {
        name: token.name,
        isAuthenticated: true,
        id: token.id,
      };

      return { ...newSession };
    },
  },
};

export const getServerAuthSession = (): Promise<IGenericSession | null> =>
  getServerSession(authOptions);
