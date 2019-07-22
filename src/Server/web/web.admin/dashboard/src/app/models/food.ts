import { Category } from './category';
import { IBaseModel } from './base.model';
import { FoodPicture } from './foodPicture';

export class Food implements IBaseModel {
  id: string;
  name: string;
  description: string;
  price: number;
  category: Category;
  categoryId: string;
  currency: string;
  pictures: FoodPicture[];
  deletedPictures: FoodPicture[];
}
