
import { ActionType } from 'typesafe-actions';
import * as foods from './foods';

export type AllActions = ActionType<typeof foods>;