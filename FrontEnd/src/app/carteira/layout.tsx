import { getServerAuthSession } from "@/server/auth";
import { redirect } from "next/navigation";

export default async function CarteiraLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const session = await getServerAuthSession();

  if (session?.user == null) {
    redirect("/");
  }

  return <div className="flex flex-col h-full">{children}</div>;
}
