import { IBaseModel } from 'app/models/base.model';
import { Observable } from 'rxjs';

export interface IBaseService<T extends IBaseModel> {

  BaseUrl: string;

  getAll(token: string): Observable<T[]>;

  get(id: string, token: string): Observable<T>;

  update(model: T, token: string): Observable<any>;

  create(model: T, token: string): Observable<any>;
}
