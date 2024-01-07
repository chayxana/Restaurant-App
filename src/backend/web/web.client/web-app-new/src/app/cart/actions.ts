'use server';

import { SessionCartItem } from "@/context/cart-context";
import { Cart, CartItem } from "@/lib/types";

export async function addItems(customerId: string, cartItems: SessionCartItem[]) {
    const cartItemsUrl = process.env.API_BASE_URL + '/basket/api/v1/items'
    const cart: Cart = {
        customer_id: customerId,
        items: cartItems.map(c => {
            return {
                food_id: c.id,
                food_name: c.name,
                unit_price: c.price,
                quantity: c.quantity
            } as CartItem;
        })
    }

    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify(cart),
    };

    await fetch(cartItemsUrl, requestOptions);
}