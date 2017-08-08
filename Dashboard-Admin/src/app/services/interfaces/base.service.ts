import { Observable } from "rxjs/Observable";
import { IBaseModel } from "app/models/base.model";

export interface IBaseService<T extends IBaseModel> {
    
    getAll(): Observable<T[]>;

    get(id: string): Observable<T>;

    update(model: T): Observable<boolean>;

    create(model: T): Observable<boolean>;
    
    baseUrl(id?: string): string;
}
