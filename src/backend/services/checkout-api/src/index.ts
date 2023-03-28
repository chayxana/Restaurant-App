import express, { Express, Request, Response } from 'express';
import dotenv from 'dotenv';
import { UserCheckout } from './model';
import valid from 'card-validator'

dotenv.config();

const app: Express = express();
const port = process.env.PORT;

app.use(express.json());

app.post('/checkout', (req: Request<{}, {}, UserCheckout>, res: Response) => {
  const card = req.body.credit_card;
  console.log(card)

  if (!valid.number(card.credit_card_number).isPotentiallyValid) {
    res.sendStatus(400)
    return
  }

  if (!valid.cvv(card.credit_card_cvv).isPotentiallyValid) {
    res.sendStatus(400)
    console.log("invalid cvv!")
    return 
  }
  if (!valid.expirationMonth(card.credit_card_expiration_year)) {
    res.sendStatus(400)
    console.log("expiration month!")
    return 
  }
  res.send('User created successfully');
});

app.listen(port, () => {
  console.log(`⚡️[server]: Server is running at http://localhost:${port}`);
});