import { IFoodDto } from "src/api/dtos/FoodDto";
import { combineReducers } from "redux";
import { FOOD_RECIEVE } from '../constants';
import { AllActions } from '../actions/actions';
import { createReducer } from 'typesafe-actions';

export interface FoodsState {
    foods: IFoodDto[];
}

const foods = createReducer([]).handleAction(FOOD_RECIEVE, (state: FoodsState, action: any) => {
    return state.foods = action.payload;
});

export const foodsReducers = combineReducers<FoodsState, AllActions>({
    foods
});

export const getFoods = (state: FoodsState) => state.foods;