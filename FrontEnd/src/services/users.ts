import api from "./axios";

class UserService {
  private readonly url = "User";

  constructor() {}

  login(email: string, password: string) {
    return api.post(this.url + "/login", {
      email,
      password,
    });
  }
}

const userService = new UserService();

export default userService;
