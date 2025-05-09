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

  createUser(user: { name: string; email: string; password: string }) {
    return api.post(this.url, user);
  }
}

const userService = new UserService();

export default userService;
