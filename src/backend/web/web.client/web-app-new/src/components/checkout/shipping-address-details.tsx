export function ShippingAddressDetails() {
  return (
    <div>
      <h2 className="mb-6 text-2xl font-bold">Shipping address</h2>

      <input
        type="text"
        name="streetAddress"
        placeholder="Address"
        className="mb-4 w-full rounded border border-gray-300 p-2"
        required
      />

      <div className="mb-4 flex gap-4">
        <input
          type="text"
          name="city"
          placeholder="City"
          className="w-full rounded border border-gray-300 p-2"
          required
        />
        <input
          type="text"
          name="state"
          placeholder="State"
          className="w-full rounded border border-gray-300 p-2"
        />
        <input
          type="text"
          name="postalCode"
          placeholder="Postal code"
          className="w-full rounded border border-gray-300 p-2"
          required
        />
      </div>
    </div>
  );
}
