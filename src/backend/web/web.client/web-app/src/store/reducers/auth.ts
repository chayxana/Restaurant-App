import { createReducer, getType } from 'typesafe-actions';
import { recieveToken } from '../actions/auth';
import { combineReducers } from 'redux';
import { AllActions } from '../actions/actions';

export interface AuthState {
    loggedIn : boolean;
}

const loggedIn = createReducer(false).handleAction(getType(recieveToken), (state: AuthState, action: any) => {
    return state.loggedIn = action.payload;
});

export const authReducers = combineReducers<AuthState, AllActions>({
    loggedIn
});