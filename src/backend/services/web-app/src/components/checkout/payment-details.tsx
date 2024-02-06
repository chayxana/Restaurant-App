export function PaymentDetails() {
  return (
    <div>
      <h2 className="mb-6 text-2xl font-bold">Payment details</h2>
      <input
        type="text"
        name="nameOnCard"
        placeholder="Name on card"
        className="mb-4 w-full rounded border border-gray-300 p-2"
        required
      />
      <input
        type="text"
        name="cardNumber"
        placeholder="Card number"
        className="mb-4 w-full rounded border border-gray-300 p-2"
        required
      />
      <div className="mb-4 flex gap-4">
        <input
          type="text"
          name="cardExpiration"
          placeholder="Expiration date (MM/YY)"
          className="w-full rounded border border-gray-300 p-2"
          required
        />
        <input
          type="text"
          name="cardCVC"
          placeholder="CVC"
          className="w-full rounded border border-gray-300 p-2"
          required
        />
      </div>
    </div>
  );
}
