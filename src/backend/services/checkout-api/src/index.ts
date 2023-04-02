import express, { Express, Request, Response } from 'express';
import dotenv from 'dotenv';
import { UserCheckout } from './model';
import pino from 'pino'
import path, { resolve } from 'path'
import * as grpc from '@grpc/grpc-js'
import * as protoLoader from '@grpc/proto-loader'
import { ProtoGrpcType } from './gen/cart';
import { GetCustomerCartResponse } from './gen/cart/GetCustomerCartResponse';


dotenv.config();

const app: Express = express();
const port = process.env.PORT ? process.env.PORT : "8080";
const host = process.env.HOST ? process.env.HOST : "127.0.0.1";
const packageDefinition = protoLoader.loadSync(path.resolve(__dirname, '../pb/cart.proto'));
const proto = grpc.loadPackageDefinition(packageDefinition) as unknown as ProtoGrpcType;

const client = new proto.cart.CartService(
  process.env.CART_URL ? process.env.CART_URL : "localhost:50003",
  grpc.credentials.createInsecure()
);

const logger = pino({
  level: process.env.LOG_LEVEL || 'info',
  formatters: {
    level: (label) => {
      return { level: label.toUpperCase() };
    },
  },
});

app.use(express.json());
app.post('/api/v1/checkout', async (req: Request<{}, {}, UserCheckout>, res: Response) => {
  logger.info(req.body)

  const response = await getCustomerCartItems(req.body.customer_id)
  logger.info(response);
  res.send('Checkout OK');
});



process.on('SIGINT', function () {
  logger.info("Gracefully shutting down from SIGINT (Ctrl-C)");
  // some other closing procedures go here
  process.exit(0);
});

app.listen(Number(port), host, () => {
  logger.info(`⚡️[server]: Server is running at http://${host}:${port}`);
});

function getCustomerCartItems(customer_id: string) {
  return new Promise<GetCustomerCartResponse>((resolve, reject) => {
    client.GetCustomerCart({ customerId: customer_id },
      (error?: grpc.ServiceError | null, serverMessage?: GetCustomerCartResponse) => {
        if (error) {
          reject(error);
        } else if (serverMessage) {
          resolve(serverMessage);
        }
      });
  });
}

