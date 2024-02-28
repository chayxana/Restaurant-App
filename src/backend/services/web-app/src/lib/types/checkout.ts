import { z } from 'zod';

export const addressSchema = z.object({
  city: z.string().trim(),
  country: z.string().trim(),
  state: z.string().trim(),
  street_address: z.string(),
  zip_code: z.number()
});

export const creditCardSchema = z.object({
  name_on_card: z.string().trim(),
  credit_card_cvv: z.number(),
  credit_card_expiration_month: z.number().min(1).max(12),
  credit_card_expiration_year: z.number().max(99),
  credit_card_number: z.string().transform(v => v.replace(/\s+/g, '')).pipe(z.string().min(1, { message: 'This field is required' }))
});;

export const checkoutScheme = z.object({
  address: addressSchema,
  credit_card: creditCardSchema,
  cart_id: z.string(),
  user_currency: z.string().default('EUR'),
  user_id: z.string(),
});

export const CheckoutResponseScheme = z.object({
  checkout_id: z.string().uuid(),
  transaction_id: z.string().uuid()
});

export type Address = z.infer<typeof addressSchema>;
export type CreditCard = z.infer<typeof creditCardSchema>;
export type Checkout = z.infer<typeof checkoutScheme>;
export type CheckoutResponse = z.infer<typeof CheckoutResponseScheme>;
