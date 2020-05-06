import * as foods from './foods';
import { combineReducers } from 'redux';
import { AllActions } from '../actions/actions';

export interface RootState {
    readonly foods: foods.FoodsState;
}

export const rootReducer = combineReducers<RootState, AllActions>({
    foods: foods.foodsReducers
});