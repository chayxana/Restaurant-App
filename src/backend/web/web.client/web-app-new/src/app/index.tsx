// pages/index.tsx
import { useEffect } from 'react';
import { useRouter } from 'next/router';

const HomePage = () => {
  const router = useRouter();

  useEffect(() => {
    router.replace('/foods');
  }, [router]);

  return null; // render nothing or a loading spinner until redirect completes
};

export default HomePage;
