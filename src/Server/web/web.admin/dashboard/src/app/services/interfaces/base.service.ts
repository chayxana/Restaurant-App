import { IBaseModel } from 'app/models/base.model';
import { Observable } from 'rxjs';

export interface IBaseService<T extends IBaseModel> {

  BaseUrl: string;

  getAll(): Observable<T[]>;

  get(id: string): Observable<T>;

  update(model: T): Observable<any>;

  create(model: T): Observable<any>;
}
