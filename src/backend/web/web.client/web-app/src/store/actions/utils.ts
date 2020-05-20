import { action } from "typesafe-actions";
import { Dispatch } from 'redux';


export const toggleMenu = () => (dispatch: Dispatch) => {
    dispatch(requestToggleMenu());
};

export const requestToggleMenu = () => action('TOGGLE_MENU');