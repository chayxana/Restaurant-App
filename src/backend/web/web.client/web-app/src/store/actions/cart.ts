import { action } from "typesafe-actions";
import { IFoodDto } from 'src/api/dtos/FoodDto';
import { Dispatch } from 'redux';
import { SHOW_CART_DIALOG, ADD_ITEM_TO_CART, DELETE_CART_ITEM, SET_QUANTITY } from '../constants';

export const showCartDialog = (show: boolean) => (dispatch: Dispatch) => {
    dispatch(requestShowCartDialog(show));
};

export const requestShowCartDialog = (show: boolean) => action(SHOW_CART_DIALOG, show);

export const requestAddItemToCart = (foodItem: IFoodDto) => action(ADD_ITEM_TO_CART, foodItem);

export const requestDeleteCartItem = (id: string) => action(DELETE_CART_ITEM, id);

export const requestSetCartItemQuantity = (quantity: number) => action(SET_QUANTITY, quantity);