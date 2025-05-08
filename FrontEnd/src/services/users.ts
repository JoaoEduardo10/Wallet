import { AxiosResponse } from "axios";
import api from "./axios";
import { Authentication } from "@/app/models/auhtntication";

class UserService {
  private readonly url = "User";

  constructor() {}

  login(
    email: string,
    password: string
  ): Promise<AxiosResponse<Authentication, Authentication>> {
    return api.post(this.url, {
      email,
      password,
    });
  }
}

const userService = new UserService();

export default userService;
