import { createAction } from 'typesafe-actions';
import { RECIEVE_TOKEN } from '../constants';

export const recieveToken = () => createAction(RECIEVE_TOKEN);