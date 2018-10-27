import { IBaseModel } from 'app/models/base.model';
import { Observable } from 'rxjs';

export interface IBaseService<T extends IBaseModel> {

    getAll(): Observable<T[]>;

    get(id: string): Observable<T>;

    update(model: T): Observable<boolean>;

    create(model: T): Observable<boolean>;

    baseUrl(id?: string): string;
}
