import { createReducer } from 'typesafe-actions';
import { combineReducers } from 'redux';
import { AllActions } from '../actions/actions';
import { RECIEVE_TOKEN } from '../constants';

export interface AuthState {
    loggedIn : boolean;
}

const loggedIn = createReducer(false).handleAction(RECIEVE_TOKEN, (state: AuthState, action: any) => {
    return state.loggedIn = action.payload;
});

export const authReducers = combineReducers<AuthState, AllActions>({
    loggedIn
});