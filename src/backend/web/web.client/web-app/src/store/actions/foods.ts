import { action } from "typesafe-actions";
import { IFoodDto } from 'src/api/dtos/FoodDto';
import { FOODS_RECIEVE, FOODS_REQUEST, FOOD_REQUEST, FOOD_RECIEVE } from '../constants';
import { Dispatch } from 'redux';
import RestClient from 'src/api/RestApiClient';

export const fetchFoods = () => (dispatch: Dispatch) => {
    dispatch(requestFoods());
    return RestClient.getFoods("token")
        .then(foods => {
            foods.forEach(f => {
                f.pictures.forEach(p => {
                    p.filePath = process.env.REACT_APP_BACKEND_URL + '/menu/' + p.filePath;
                });
            });
            dispatch(receiveFoods(foods));
        });
};

export const fetchFood = (foodId: string) => (dispatch: Dispatch) => {
    dispatch(requestFood());
    return RestClient.getFood("token", foodId)
        .then(food => {
            food.pictures.forEach(p => {
                p.filePath = process.env.REACT_APP_BACKEND_URL + '/menu/' + p.filePath;
            });
            dispatch(receiveFood(food));
        });
};

export const requestFoods = () => action(FOODS_REQUEST);

export const receiveFoods = (foods: IFoodDto[]) => action(FOODS_RECIEVE, foods);

export const requestFood = () => action(FOOD_REQUEST);

export const receiveFood = (food: IFoodDto) => action(FOOD_RECIEVE, food); 
