import { IBaseModel } from './base.model';

export class Category implements IBaseModel {
    id: string;
    name: string;
    color: string;
}
