export function ContactDetails() {
  return (
    <div>
      <h2 className="mb-6 text-2xl font-bold">Contact information</h2>
      <input
        type="email"
        name="email"
        placeholder="Email address"
        className="mb-4 w-full rounded border border-gray-300 p-2"
        required
      />
    </div>
  );
}
