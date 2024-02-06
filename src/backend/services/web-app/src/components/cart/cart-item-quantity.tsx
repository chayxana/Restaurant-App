type Props = {
  onQuantityChange: (_newQuantity: number) => void;
  quantity: number;
  className?: string;
};

export const CartItemQuantity: React.FC<Props> = ({ quantity, onQuantityChange, className }) => {
  const handleQuantityChange = (newQuantity: number) => {
    if (newQuantity > 0) {
      onQuantityChange(newQuantity);
    }
  };

  return (
    <div className={className}>
      <button
        onClick={() => handleQuantityChange(quantity - 1)}
        className="text-md rounded-l bg-gray-200 px-2 py-1 text-gray-600"
      >
        -
      </button>
      <input
        type="text"
        className="w-12 border-b border-t text-center"
        value={quantity}
        onChange={(e) => handleQuantityChange(parseInt(e.target.value) || 0)}
      />
      <button
        onClick={() => handleQuantityChange(quantity + 1)}
        className="text-md rounded-r bg-gray-200 px-2 py-1 text-gray-600"
      >
        +
      </button>
    </div>
  );
};
