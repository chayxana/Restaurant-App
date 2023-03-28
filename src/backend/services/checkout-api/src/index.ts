import express, { Express, Request, Response } from 'express';
import dotenv from 'dotenv';
import { UserCheckout } from './model';
import valid from 'card-validator'
import pino from 'pino'

dotenv.config();

const app: Express = express();
const port = process.env.PORT;

app.use(express.json());

const logger = pino({
  level: process.env.LOG_LEVEL || 'info',
  formatters: {
    level: (label) => {
      return { level: label.toUpperCase() };
    },
  },
});

app.post('/checkout', (req: Request<{}, {}, UserCheckout>, res: Response) => {
  const card = req.body.credit_card;
  console.log(card)

  if (!valid.number(card.credit_card_number).isPotentiallyValid) {
    res.sendStatus(400);
    logger.warn("invalid Card Number");
    return
  }

  if (!valid.cvv(card.credit_card_cvv, 5).isPotentiallyValid) {
    res.sendStatus(400);
    logger.warn("invalid card CVV");
    return
  }
  if (!valid.expirationMonth(card.credit_card_expiration_year)) {
    res.sendStatus(400);
    logger.warn("invalid card expiration year");
    return
  }
  res.send('User created successfully');
});

app.listen(port, () => {
  console.log(`⚡️[server]: Server is running at http://localhost:${port}`);
});