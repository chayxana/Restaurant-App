import { createReducer, getType } from 'typesafe-actions';
import { requestShowCartDialog, requestAddItemToCart } from '../actions/cart';
import { combineReducers } from 'redux';
import { AllActions } from '../actions/actions';
import { IFoodDto } from 'src/api/dtos/FoodDto';

export interface CartState {
    showCartDialog: boolean;
    cartItems: IFoodDto[];
}

const showCart = createReducer(false)
                    .handleAction(getType(requestShowCartDialog), (state: CartState, action: any) => {
    return state.showCartDialog = action.payload;
});

const cartItems = createReducer([]).handleAction(getType(requestAddItemToCart), (state: CartState, action: any) => {
    return state.cartItems = action.any;
});


export const cartReducers = combineReducers<CartState, AllActions>({
    showCartDialog: showCart,
    cartItems
});