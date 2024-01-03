type Props = {
  onQuantityChange: (newQuantity: number) => void;
  quantity: number;
  className?: string;
};

export const CartItemQuantity: React.FC<Props> = ({
  quantity,
  onQuantityChange,
  className,
}) => {
  const handleQuantityChange = (newQuantity: number) => {
    if (newQuantity > 0) {
      onQuantityChange(newQuantity);
    }
  };

  return (
    <div className={className}>
      <button
        onClick={() => handleQuantityChange(quantity - 1)}
        className="text-md bg-gray-200 text-gray-600 px-2 py-1 rounded-l"
      >
        -
      </button>
      <input
        type="text"
        className="w-12 text-center border-t border-b"
        value={quantity}
        onChange={(e) => handleQuantityChange(parseInt(e.target.value) || 0)}
      />
      <button
        onClick={() => handleQuantityChange(quantity + 1)}
        className="text-md bg-gray-200 text-gray-600 px-2 py-1 rounded-r"
      >
        +
      </button>
    </div>
  );
};
