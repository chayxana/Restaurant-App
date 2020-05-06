import { RootState } from './redusers';
import * as foods from './foods';

export const getFoods = (state: RootState) => foods.getFoods(state.foods); 