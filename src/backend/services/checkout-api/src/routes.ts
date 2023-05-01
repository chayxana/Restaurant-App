import express, { Request, Response } from "express";
import { UserCheckout } from "./model";
import Checkout from "./checkout";

const router = express.Router();

router.post('/api/v1/checkout', async (req: Request<{}, {}, UserCheckout>, res: Response) => {
    try {
      Checkout(req.body); 
    } catch (error) {
        res.status(500).send({ error })
        return
    }
    res.send('Checkout OK');
});

export default router;