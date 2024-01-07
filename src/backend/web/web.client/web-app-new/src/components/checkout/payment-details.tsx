export function PaymentDetails() {
  return (
    <div>
      <h2 className="mb-6 text-2xl font-bold">Payment details</h2>
      <input
        type="text"
        name="nameOnCard"
        placeholder="Name on card"
        // value={formData.nameOnCard}
        // onChange={handleChange}
        className="mb-4 w-full rounded border border-gray-300 p-2"
        required
      />
      <input
        type="text"
        name="cardNumber"
        placeholder="Card number"
        // value={formData.cardNumber}
        // onChange={handleChange}
        className="mb-4 w-full rounded border border-gray-300 p-2"
        required
      />
      <div className="mb-4 flex gap-4">
        <input
          type="text"
          name="cardExpiration"
          placeholder="Expiration date (MM/YY)"
          //   value={formData.cardExpiration}
          //   onChange={handleChange}
          className="w-full rounded border border-gray-300 p-2"
          required
        />
        <input
          type="text"
          name="cardCVC"
          placeholder="CVC"
          //   value={formData.cardCVC}
          //   onChange={handleChange}
          className="w-full rounded border border-gray-300 p-2"
          required
        />
      </div>
    </div>
  );
}
