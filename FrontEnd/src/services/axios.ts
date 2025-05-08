import axios from "axios";
import https from "https";

const isDev = process.env.NODE_ENV === "development";

const httpsAgent = new https.Agent({
  rejectUnauthorized: !isDev, // Só ignora em dev
});

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  httpsAgent,
});

export default api;
