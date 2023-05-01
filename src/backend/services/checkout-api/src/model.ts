export interface UserCheckout {
    address: Address;
    credit_card: CreditCard;
    customer_id: string;
    email: string;
    user_currency: string;
}

export interface Address {
    city: string;
    country: string;
    state: string;
    street_address: string;
    zip_code: number;
}

export interface CreditCard {
    credit_card_cvv: number;
    credit_card_expiration_month: number;
    credit_card_expiration_year: number;
    credit_card_number: string;
}

export interface CustomerCart {
    customer_id: string;
    items?: CartItem[];
}

export interface CartItem {
    item_id: string;
    price: number;
    quantity: number;
}

export interface CheckoutEvent {
    transaction_id?: string;
    user_checkout: UserCheckout;
    customer_cart: CustomerCart
}