import { z } from 'zod';

export const AddressSchema = z.object({
    city: z.string().min(1, 'City is required'),
    country: z.string(),
    state: z.string(),
    street_address: z.string().min(1, 'Address is required'),
    zip_code: z.number(),
})

export const CreditCardSchema = z.object({
    name_on_card: z.string().min(1, 'Name on card is required'),
    credit_card_cvv: z.number(),
    credit_card_expiration_month: z.number().min(1, 'Invalid month').max(12, 'Invalid month'),
    credit_card_expiration_year: z.number().max(99),
    credit_card_number: z.string(),
})

export const UserCheckoutSchema = z.object({
    address: AddressSchema,
    credit_card: CreditCardSchema,
    cart_id: z.string().min(1, 'Cart ID cannot be empty'),
    user_currency: z.string().default('EUR'),
    user_id: z.string().min(1, 'User ID cannot be empty'),
})

export type UserCheckoutReq = z.infer<typeof UserCheckoutSchema>;


export interface Cart {
    cart_id: string;
    items?: CartItem[];
}

export interface CartItem {
    item_id: string;
    price: number;
    quantity: number;
}


const CheckoutSchemaWithoutCreditCard = UserCheckoutSchema.omit({ credit_card: true })
type CheckoutSchemaWithoutCreditCard = z.infer<typeof CheckoutSchemaWithoutCreditCard>

export interface CheckoutEvent {
    checkout_id: string;
    transaction_id: string;
    user_checkout: CheckoutSchemaWithoutCreditCard;
    customer_cart: Cart
}