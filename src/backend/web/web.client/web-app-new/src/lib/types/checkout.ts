import { z } from 'zod';

const addressSchema = z.object({
  city: z.string(),
  country: z.string(),
  state: z.string(),
  street_address: z.string(),
  zip_code: z.number()
});

const creditCardSchema = z.object({
  credit_card_cvv: z.string(),
  credit_card_expiration_month: z.string(),
  credit_card_expiration_year: z.string(),
  credit_card_number: z.string()
});

export const checkoutScheme = z.object({
  address: addressSchema,
  credit_card: creditCardSchema,
  customer_id: z.string(),
  email: z.string().email(),
  user_currency: z.string()
});

export type Checkout = z.infer<typeof checkoutScheme>;