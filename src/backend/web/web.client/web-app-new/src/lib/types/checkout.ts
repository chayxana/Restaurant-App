import { z } from 'zod';

const addressSchema = z.object({
  city: z.string(),
  country: z.string(),
  state: z.string(),
  street_address: z.string(),
  zip_code: z.number()
});

export const creditCardSchema = z.object({
  name_on_card: z.string(),
  credit_card_cvv: z.string().max(3),
  credit_card_expiration_month: z.number().min(1).max(12),
  credit_card_expiration_year: z.number(),
  credit_card_number: z.string()
});

export type CreditCard = z.infer<typeof creditCardSchema>;

export const checkoutScheme = z.object({
  address: addressSchema,
  credit_card: creditCardSchema,
  customer_id: z.string(),
  email: z.string().email(),
  user_currency: z.string()
});

export type Checkout = z.infer<typeof checkoutScheme>;
