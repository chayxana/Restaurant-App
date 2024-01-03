import Image from "next/image";
import { TrashIcon } from "@heroicons/react/24/solid";
import { useCart } from "@/context/CartContext";
import { CartItemQuantity } from "./CartItemQuantity";

// Type for the cart item
type CartItemProps = {
  id: number;
  name: string;
  description: string;
  quantity: number;
  price: number;
  image: string;
};

// A single cart item component
export const CartItem: React.FC<CartItemProps> = ({
  id,
  name,
  description,
  quantity,
  price,
  image,
}) => {
  const { removeItem, setQuantity } = useCart();
  return (
    <div className="flex items-center justify-between border-b border-gray-200 py-4">
      <div className="flex items-center">
        <Image
          src={image}
          alt={name}
          quality={75}
          width={60}
          height={60}
          className="rounded-full"
        />
        <div className="ml-4">
          <h3 className="text-lg font-bold">{name}</h3>
          <p className="text-sm text-gray-600">{description}</p>
          <p className="text-sm font-bold">Total: ${price.toFixed(2)}</p>
        </div>
      </div>
      <div className="flex items-center">
        <CartItemQuantity
          className="px-4 relative"
          quantity={quantity}
          onQuantityChange={(newQuantity: number) => {
            setQuantity(id, newQuantity);
          }}
        />
        <button
          className="text-gray-500 hover:text-red-500"
          onClick={() => removeItem(id)}
        >
          <TrashIcon className="h-6 w-6" />
        </button>
      </div>
    </div>
  );
};
