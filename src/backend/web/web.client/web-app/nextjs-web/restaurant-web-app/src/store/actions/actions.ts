
import { ActionType } from 'typesafe-actions';
import * as foods from './foods';
import * as utils from './utils';
import * as cart from './cart';

export type AllActions = ActionType<typeof foods | typeof utils | typeof cart>;

