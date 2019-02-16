
import {throwError as observableThrowError,  Observable } from 'rxjs';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { map, catchError } from 'rxjs/operators';
import { IBaseModel } from 'app/models/base.model';
import { IBaseService } from 'app/services/interfaces/base.service';

export abstract class BaseService<T extends IBaseModel> implements IBaseService<T> {
    protected headers: Headers;
    protected options: RequestOptions;

    constructor(private http: Http) {
        this.headers = new Headers({ 'Content-Type': 'application/json' });
        this.options = new RequestOptions({ headers: this.headers });
    }

    abstract baseUrl(id?: string): string;

    getAll(): Observable<T[]> {

        return this.http.get(this.baseUrl(), this.options)
            .pipe(map(this.extractData), catchError(this.handleError));
    }

    get(id: string): Observable<T> {
        return this.http.get(this.baseUrl(id), this.options)
            .pipe(map(this.extractData), catchError(this.handleError));
    }

    update(model: T): Observable<boolean> {
        return this.http.put(this.baseUrl(model.id), model, this.options)
            .pipe(map(r => r.ok), catchError(this.handleError));
    }

    create(model: T): Observable<boolean> {
        return this.http.post(this.baseUrl(), model, this.options)
            .pipe(map(x => x.ok), catchError(this.handleError));
    }

    delete(model: T): Observable<boolean> {
        return this.http.delete(this.baseUrl(model.id), this.options)
            .pipe(map(x => x.ok), catchError(this.handleError));
    }

    protected extractData(res: Response) {
        const body = res.json();
        return body || {};
    }

    protected handleError(error: Response | any) {
        // In a real world app, you might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return observableThrowError(errMsg);
    }
}
