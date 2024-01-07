import { authOptions } from '@/lib/auth';
import { getServerSession } from 'next-auth';

export async function checkoutServer() {
  const session = await getServerSession(authOptions);
  if (!session?.user) {
    throw new Error('User is not authenticated');
  }
}
