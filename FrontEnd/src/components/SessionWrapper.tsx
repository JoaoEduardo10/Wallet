"use client";

import { SessionProvider } from "next-auth/react";

interface SessionWrapperProps {
  children: React.ReactNode;
}

const SessionWrapper = ({ children }: SessionWrapperProps) => {
  return <SessionProvider>{children}</SessionProvider>;
};

export default SessionWrapper;
