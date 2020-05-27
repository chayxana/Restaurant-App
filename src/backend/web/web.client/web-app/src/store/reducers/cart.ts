import { createReducer } from 'typesafe-actions';
import { combineReducers } from 'redux';
import { AllActions } from '../actions/actions';
import { IFoodDto } from 'src/api/dtos/FoodDto';
import { ADD_ITEM_TO_CART, SHOW_CART_DIALOG } from '../constants';

export interface CartState {
    showCartDialog: boolean;
    cartItems: IFoodDto[];
}

const showCart = createReducer(false)
    .handleAction(SHOW_CART_DIALOG, (state: CartState, action: any) => {
    return state.showCartDialog = action.payload;
});

const cartItems = createReducer([]).handleAction(ADD_ITEM_TO_CART, (state: CartState, action: any) => {
    return state.cartItems = action.any;
});


export const cartReducers = combineReducers<CartState, AllActions>({
    showCartDialog: showCart,
    cartItems
});