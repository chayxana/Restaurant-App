import express, { Request, Response } from "express";
import { UserCheckoutReq, UserCheckoutSchema } from "./model";
import Checkout from "./checkout/checkout";

const router = express.Router();

router.post('/api/v1/checkout', async (req: Request, res: Response) => {
  try {
    const checkoutReq: UserCheckoutReq = UserCheckoutSchema.parse(req.body)
    const userId = 'anon';

    const checkout = await Checkout(checkoutReq, userId);
    res.send({
      checkout_id: checkout.checkout_id,
      transaction_id: checkout.transaction_id,
    });
  } catch (error) {
    res.status(500).send({ error_message: error })
  }
});

export default router;
