
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { IBaseModel } from 'app/models/base.model';
import { IBaseService } from 'app/services/interfaces/base.service';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

export abstract class BaseService<T extends IBaseModel> implements IBaseService<T> {
    protected headers: HttpHeaders;
    protected options: any;

    constructor(private http: HttpClient) {
        this.headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        this.options = {
          headers: this.headers
        };
    }

    abstract get BaseUrl(): string;

    getAll(): Observable<T[]> {

        return this.http.get(this.BaseUrl, this.options)
            .pipe(map(this.extractData), catchError(this.handleError));
    }

    get(id: string): Observable<T> {
        return this.http.get(`${this.BaseUrl}/${id}`, this.options)
            .pipe(map(this.extractData), catchError(this.handleError));
    }

    update(model: T): Observable<any> {
        const result = this.http.put(`${this.BaseUrl}/${model.id}`, model, this.options)
                          .pipe(catchError(this.handleError));
        return result;
    }

    create(model: T): Observable<any> {
        return this.http.post(this.BaseUrl, model, this.options)
            .pipe(catchError(this.handleError));
    }

    delete(model: T): Observable<any> {
        return this.http.delete(`${this.BaseUrl}/${model.id}`, this.options)
            .pipe(catchError(this.handleError));
    }

    protected extractData(res: any) {
      return res;
    }

    protected handleError(error: HttpErrorResponse) {
      if (error.error instanceof ErrorEvent) {
        // A client-side or network error occurred. Handle it accordingly.
        console.error('An error occurred:', error.error.message);
      } else {
        // The backend returned an unsuccessful response code.
        // The response body may contain clues as to what went wrong,
        console.error(
          `Backend returned code ${error.status}, ` +
          `body was: ${error.error}`);
      }
      // return an observable with a user-facing error message
      return throwError(
        'Something bad happened; please try again later.');
    }
}
