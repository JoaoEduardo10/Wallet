import axios from "axios";

function treatErrorAxios(error: unknown) {
  let message = "Error desconhecido";

  if (axios.isAxiosError(error)) {
    if (error.response?.data) {
      message = error.response?.data;
    }
  }

  return message;
}

export { treatErrorAxios };
