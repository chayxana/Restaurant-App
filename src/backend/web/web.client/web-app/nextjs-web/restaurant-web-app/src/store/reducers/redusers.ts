import * as foods from './foods';
import * as cart from './cart';
import * as auth from './auth';

import { combineReducers } from 'redux';
import { AllActions } from '../actions/actions';

export interface RootState {
    readonly foods: foods.FoodsState;
    readonly cart: cart.CartState;
    readonly auth: auth.AuthState;
}

export const rootReducer = combineReducers<RootState, AllActions>({
    foods: foods.foodsReducers,
    cart: cart.cartReducers,
    auth: auth.authReducers
});