import getCustomerCartItems from "./cartService";
import { randomUUID } from "crypto";
import express, { Request, Response } from "express";
import { logger } from "./logger";
import { UserCheckout } from "./model";
import Payment from "./paymentService";

const router = express.Router();

router.post('/api/v1/checkout', async (req: Request<{}, {}, UserCheckout>, res: Response) => {
    try {
        const checkoutID = randomUUID().toString();
        const cart = await getCustomerCartItems(req.body.customer_id)
        const pay = await Payment(checkoutID, req.body, cart!!.items!!)
        logger.info(pay?.transactionId)
    } catch (error) {
        res.status(500).send({ error: error })
        return
    }
    res.send('Checkout OK');
});

export default router;