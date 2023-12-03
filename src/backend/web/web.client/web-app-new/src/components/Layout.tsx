import React from 'react';
import Navbar from './NavBar';

export default function Layout({ children }: {
    children: React.ReactNode
  }) {
    return (
      <>
        <Navbar />
        <main>{children}</main>
      </>
    )
  }