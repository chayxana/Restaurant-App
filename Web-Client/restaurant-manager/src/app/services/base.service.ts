import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http'
import { Observable } from 'rxjs/Observable';

export abstract class BaseService<T> {
    protected headers: Headers;
    protected options: RequestOptions;

    constructor(private http: Http) {
        this.headers = new Headers({ 'Content-Type': 'application/json' });
        this.options = new RequestOptions({ headers: this.headers });
    }

    abstract baseUrl(): string;

    getAll(): Observable<T[]> {
        
        return this.http.get(this.baseUrl())
            .map(this.extractData)
            .catch(this.handleError);
    }

    get(id: string): Observable<T> {
        return null;
    }

    update(model: T) {
        return null;
    }

    create(model: T) {
        return null;
    }


    public extractData(res: Response) {
        let body = res.json();
        return body || {};
    }

    public handleError(error: Response | any) {
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
        return Observable.throw(errMsg);
    }
}