import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  reactStrictMode: true,
  webpack(config) {
    if (process.env.NODE_ENV === "development") {
      // Desabilita a verificação do certificado autoassinado em desenvolvimento
      process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = "0";
    }
    return config;
  },
};

export default nextConfig;
