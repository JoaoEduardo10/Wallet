import { getServerAuthSession } from "@/server/auth";
import { redirect } from "next/navigation";

export default async function AuthentiocationLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const session = await getServerAuthSession();

  if (session !== null) {
    redirect("/carteira");
  }

  return <div className="flex flex-col h-full">{children}</div>;
}
