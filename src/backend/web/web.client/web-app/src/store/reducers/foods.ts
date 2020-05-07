import { IFoodDto } from "src/api/dtos/FoodDto";
import { combineReducers } from "redux";
import { FOODS_RECIEVE, FOOD_RECIEVE } from '../constants';
import { AllActions } from '../actions/actions';
import { createReducer } from 'typesafe-actions';

export interface FoodsState {
    foods: IFoodDto[];
    food: IFoodDto;
}


const foods = createReducer([]).handleAction(FOODS_RECIEVE, (state: FoodsState, action: any) => {
    return state.foods = action.payload;
});

const food = createReducer({}).handleAction(FOOD_RECIEVE, (state: FoodsState, action: any) => {
    return state.food = action.payload;
});

export const foodsReducers = combineReducers<FoodsState, AllActions>({
    foods,
    food
});

export const getFoods = (state: FoodsState) => state.foods;