import { BaseModel } from "app/models/base.model";

export class Category implements BaseModel {
    id: string;
    name: string;
    color: string;
}
