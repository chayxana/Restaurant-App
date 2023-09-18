import express, { Request, Response } from "express";
import { UserCheckout } from "./model";
import Checkout from "./checkout";

const router = express.Router();

router.post('/api/v1/checkout', async (req: Request<{}, {}, UserCheckout>, res: Response) => {
    try {
      Checkout(req.body);
      res.send('Checkout OK');
    } catch (error) {
      res.status(500).send({ error })
    }
});

export default router;