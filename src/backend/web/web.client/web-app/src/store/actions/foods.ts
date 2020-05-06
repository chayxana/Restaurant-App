import { action } from "typesafe-actions";
import { IFoodDto } from 'src/api/dtos/FoodDto';
import { FOOD_RECIEVE, FOOD_REQUEST } from '../constants';
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
export const requestFoods = () => action(FOOD_REQUEST);

export const receiveFoods = (foods: IFoodDto[]) => action(FOOD_RECIEVE, foods);

