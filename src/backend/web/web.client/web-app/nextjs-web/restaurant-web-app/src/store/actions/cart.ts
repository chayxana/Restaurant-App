import { action } from "typesafe-actions";
import { Dispatch } from 'redux';
import { SHOW_CART_DIALOG, ADD_ITEM_TO_CART, DELETE_CART_ITEM, SET_QUANTITY } from '../constants';
import { IFoodDto } from "@/api/dtos/FoodDto";

export const showCartDialog = (show: boolean) => (dispatch: Dispatch) => {
    dispatch(requestShowCartDialog(show));
};

export const requestShowCartDialog = (show: boolean) => action(SHOW_CART_DIALOG, show);

export const requestAddItemToCart = (foodItem: IFoodDto) => action(ADD_ITEM_TO_CART, foodItem);

export const requestDeleteCartItem = (id: string) => action(DELETE_CART_ITEM, id);

export const requestSetCartItemQuantity = (quantity: number) => action(SET_QUANTITY, quantity);