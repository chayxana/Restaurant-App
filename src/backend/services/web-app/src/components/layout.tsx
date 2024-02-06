import React from 'react';
import Navbar from './navbar';

export default function Layout({ children }: { children: React.ReactNode }) {
  return (
    <div className="flex flex-col md:flex-row">
      <Navbar />
      <main className="flex-1 pt-16">{children}</main>
    </div>
  );
}
