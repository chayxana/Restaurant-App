import { Category } from "./category";
import { BaseModel } from "app/models/base.model";

export class Food implements BaseModel {
    id: string;
    name: string;
    description: string;
    price: number;
    category: Category
}
