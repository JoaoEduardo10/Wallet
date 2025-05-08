import { Session } from "next-auth";

export default interface IGenericSession extends Session {
  acessToken: string;
}
