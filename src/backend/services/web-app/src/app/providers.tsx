'use client';
import { PropsWithChildren } from 'react';
import { SessionProvider } from 'next-auth/react';

export const Providers = ({ children }: PropsWithChildren<{}>) => {
  return (
    <SessionProvider>
      <>{children}</>
    </SessionProvider>
  );
};
