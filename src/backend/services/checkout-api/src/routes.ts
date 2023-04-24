import getCustomerCartItems from "./cartService";
import { randomUUID } from "crypto";
import express, { Request, Response } from "express";
import { logger } from "./logger";
import { CartItem, CheckoutEvent, UserCheckout } from "./model";
import Payment from "./paymentService";
import checkoutPublisher from "./publisher";

const router = express.Router();

router.post('/api/v1/checkout', async (req: Request<{}, {}, UserCheckout>, res: Response) => {
    try {
        const checkoutID = randomUUID().toString();
        const cart = await getCustomerCartItems(req.body.customer_id)
        const pay = await Payment(checkoutID, req.body, cart!!.items!!)
        logger.info(pay?.transactionId)

        const cartItems = cart?.items?.map<CartItem>((i) => {
            return{
                item_id: i.itemId?.toString(),
                price: i.price,
                quantity: i.quantity?.toString()
            } as CartItem
        })
        const checkoutEvent: CheckoutEvent = {
            transaction_id: pay.transactionId,
            user_checkout: req.body,
            customer_cart: {
                customer_id: cart?.customerId!!,
                items: cartItems,
            }
        }
        logger.info("checkout-event: ", checkoutEvent)
        await checkoutPublisher.Publish(checkoutEvent)
    } catch (error) {
        res.status(500).send({ error })
        return
    }
    res.send('Checkout OK');
});

export default router;