import { IBaseModel } from "app/models/base.model";

export class Category implements IBaseModel {
    id: string;
    name: string;
    color: string;
}
