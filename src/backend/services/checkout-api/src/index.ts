import router from './routes';
import { logger } from './logger';
import { config } from './config';


import express from 'express';

const app = express();
app.use(express.json());

app.use(config.baseUrl, router)

app.listen(Number(config.port), config.host, () => {
  logger.info(`⚡️[server]: Server is running at http://${config.host}:${config.port}`);
});

process.on('SIGINT', () => {
  logger.info("Gracefully shutting down from SIGINT (Ctrl-C)");
  // some other closing procedures go here
  process.exit(0);
});
