import express, { Request, Response } from "express";
import { UserCheckoutReq } from "./model";
import Checkout from "./checkout/checkout";

const router = express.Router();

router.post('/api/v1/checkout', async (req: Request<{}, {}, UserCheckoutReq>, res: Response) => {
  try {
    const checkout = await Checkout(req.body);
    res.send({
      checkout_id: checkout.checkout_id,
      transaction_id: checkout.transaction_id,
    });
  } catch (error) {
    res.status(500).send({ error_message: error })
  }
});

export default router;
