import { randomUUID } from "crypto";
import { CartItem, CheckoutEvent, UserCheckoutReq } from "../models/model";
import getCustomerCartItems from "../cart/cartService";
import { logger } from "../utils/logger";
import checkoutPublisher from "../messagging/publisher";
import { trace, context, ROOT_CONTEXT, SpanStatusCode } from "@opentelemetry/api";
import { pay } from "../payment/paymentService";

export default async function Checkout(checkout: UserCheckoutReq, userId: string): Promise<CheckoutEvent> {
    const span = trace.getTracer('checkout-api').startSpan('checkout-api.Checkout');
    return await context.with(trace.setSpan(ROOT_CONTEXT, span), async () => {
        try {
            const cart = await getCustomerCartItems(checkout.cart_id);

            const checkoutID = randomUUID().toString();
            const payed = await pay(cart.items, checkout, checkoutID, userId);

            logger.debug('payment transaction succeded', { transaction_id: payed.transactionId })
            const cartItems = cart.items.map<CartItem>((i) => {
                return {
                    item_id: i.itemId?.toString(),
                    price: i.price,
                    quantity: Number(i.quantity)
                } as CartItem
            })

            const checkoutEvent: CheckoutEvent = {
                checkout_id: checkoutID,
                transaction_id: payed.transactionId,
                user_checkout: checkout,
                customer_cart: {
                    cart_id: cart.cartId,
                    items: cartItems,
                }
            }
            logger.debug("checkout-event", { event: checkoutEvent })
            await checkoutPublisher.Publish(checkoutEvent)
            return checkoutEvent;
        } catch (error) {
            span.recordException(error as Error);
            span.setStatus({
                code: SpanStatusCode.ERROR,
                message: (error as Error)?.message,
            });
            throw error
        } finally {
            span.end();
        }
    });
}