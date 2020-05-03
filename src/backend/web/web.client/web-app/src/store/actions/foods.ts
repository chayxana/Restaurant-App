import { createAction } from "typesafe-actions"
import { FoodDto } from 'src/api/dtos/FoodDto'
import { FOOD_RECIEVE } from '../constants'


export const receiveFoods = createAction(FOOD_RECIEVE, action => {
    return (foods: FoodDto[]) => action(foods);
});
