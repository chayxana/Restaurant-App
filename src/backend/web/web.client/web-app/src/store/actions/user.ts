import { action } from "typesafe-actions";
import { Dispatch } from 'redux';

export const logOut = () => (dispatch: Dispatch) => {
    dispatch(requestLogOut()); 
};

export const requestLogOut = () => action('LOG_OUT');