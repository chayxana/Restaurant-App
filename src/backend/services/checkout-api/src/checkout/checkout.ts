import { randomUUID } from "crypto";
import { CartItem, CheckoutEvent, UserCheckoutReq } from "../model";
import getCustomerCartItems from "../cart/cartService";
import Payment from "../payment/paymentService";
import { logger } from "../logger";
import checkoutPublisher from "../messagging/publisher";
import { trace, context, ROOT_CONTEXT, SpanStatusCode } from "@opentelemetry/api";

export default async function Checkout(checkout: UserCheckoutReq): Promise<CheckoutEvent> {
    const span = trace.getTracer('checkout-api').startSpan('checkout-api.Checkout');
    return await context.with(trace.setSpan(ROOT_CONTEXT, span), async () => {
        try {
            const checkoutID = randomUUID().toString();
            const cart = await getCustomerCartItems(checkout.customer_id)
            const pay = await Payment(checkoutID, checkout, cart!!.items!!)
            logger.debug('payment transaction succeded', { transaction_id: pay?.transactionId })

            const cartItems = cart?.items?.map<CartItem>((i) => {
                return {
                    item_id: i.itemId?.toString(),
                    price: i.price,
                    quantity: Number(i.quantity)
                } as CartItem
            })

            const checkoutEvent: CheckoutEvent = {
                checkout_id: checkoutID,
                transaction_id: pay.transactionId,
                user_checkout: checkout,
                customer_cart: {
                    customer_id: cart?.customerId!!,
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