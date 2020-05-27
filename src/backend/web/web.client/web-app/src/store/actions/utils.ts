import { action } from "typesafe-actions";
import { Dispatch } from 'redux';
import { TOGGLE_MENU } from '../constants';


export const toggleMenu = () => (dispatch: Dispatch) => {
    dispatch(requestToggleMenu());
};

export const requestToggleMenu = () => action(TOGGLE_MENU);