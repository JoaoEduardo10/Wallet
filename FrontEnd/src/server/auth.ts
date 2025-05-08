import { Authentication } from "@/app/models/auhtntication";
import { treatErrorAxios } from "@/helpers/treat-error-axios";
import UserService from "@/services/users";
import { DefaultSession, getServerSession, NextAuthOptions } from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";
import { Session } from "next-auth";
import { JWT } from "next-auth/jwt";
import IGenericSession from "@/app/models/Dtos/session";

const credentialsProvider = CredentialsProvider({
  name: "wallet",
  credentials: {
    email: { label: "Email", type: "email", placeholder: "email" },
    password: { label: "Password", type: "password" },
  },

  async authorize(Credentials) {
    try {
      const { email, password } = Credentials!;

      const response = await UserService.login(email, password);

      return response.data;
    } catch (error: unknown) {
      const message = treatErrorAxios(error);

      console.log(message);

      return null;
    }
  },
});

export const authOptions: NextAuthOptions = {
  secret: process.env.NEXTAUTH_SECRET,
  session: {
    strategy: "jwt",
    maxAge: 3600,
  },
  providers: [credentialsProvider],
  callbacks: {
    jwt: async ({ token, user }) => {
      const auth = user as unknown as Authentication;

      const isSignIN = auth.isAuthenticated;
      const actualDateInSeconds = Math.floor(Date.now() / 1000);

      if (isSignIN) {
        if (!user) {
          return Promise.resolve({});
        }

        token.name = auth.username;
        token.jwt = auth.token;
        token.expiration = auth.tokenExpirationTime;
      }

      const time = token.expiration as unknown as number;

      if (!time) {
        return Promise.resolve({});
      }

      if (actualDateInSeconds > time) {
        return Promise.resolve({});
      }

      return Promise.resolve(token);
    },

    async session({
      session,
      token,
    }: {
      session: Session;
      token: JWT;
    }): Promise<Session | DefaultSession> {
      if (!token || !token.jwt || !session) {
        return { ...session, user: {} };
      }

      const newSession = { ...session, acessToken: "" };

      newSession.acessToken = token.jwt as string;
      newSession.user = {
        name: token.name,
      };

      return { ...newSession };
    },
  },
};

export const getServerAuthSession = (): Promise<IGenericSession | null> =>
  getServerSession(authOptions);
