'use client';

import { SessionCartItem, useCart } from '@/context/cart-context';
// import { useFormState } from 'react-dom';
import { addItemServer } from './actions';
import { SubmitButton } from './submit-button';
import { useSession } from 'next-auth/react';
import { FoodItem } from '@/lib/types/food-item';

export function AddToCart({ quantity, foodItem }: { quantity: number; foodItem: FoodItem }) {
  //   const [message, formAction] = useFormState(addItemServer, null);
  const { addItem } = useCart();
  const { status, data } = useSession();

  const cartItemSession: SessionCartItem = {
    id: foodItem.id,
    name: foodItem.name,
    description: foodItem.description,
    quantity,
    price: foodItem.price,
    image: foodItem.image
  };
  return (
    <form
      action={async (_formData: FormData) => {
        addItem({ ...cartItemSession });
        if (status === 'authenticated') {
          await addItemServer(data.user.user_id, { ...cartItemSession });
        }
      }}
    >
      {/* <p aria-live="polite" className="sr-only" role="status">
        {message}
      </p> */}
      <SubmitButton />
    </form>
  );
}
