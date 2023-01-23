import { Food } from './food';
import * as uuid from 'uuid';

export function getFoodInstance(): Food {
  return <Food>{
    id: uuid.v4(),
    name: '',
    description: '',
    price: null,
    category: null,
    categoryId: null,
    pictures: [],
    deletedPictures: [],
    currency: null
  };
}