'use client';

import { SessionCartItem, useCart } from '@/context/cart-context';
// import { useFormState } from 'react-dom';
import { addCartItem } from './actions';
import { useFormStatus } from 'react-dom';

function SubmitButton() {
  const { pending } = useFormStatus();
  return (
    <div className="px-6 pb-2 pt-4">
      <button
        onClick={(e: React.FormEvent<HTMLButtonElement>) => {
          if (pending) e.preventDefault();
        }}
        className="w-full rounded bg-orange-500 px-4 py-2 font-bold text-white hover:bg-orange-700"
      >
        Add to Cart
      </button>
    </div>
  );
}

export function AddToCart({ item }: { item: SessionCartItem }) {
  //   const [message, formAction] = useFormState(addItemServer, null);
  const { increment } = useCart();
  return (
    <form
      action={async (_formData: FormData) => {
        increment();
        await addCartItem({ ...item });
      }}
    >
      <SubmitButton />
    </form>
  );
}
