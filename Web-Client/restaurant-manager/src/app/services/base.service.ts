import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http'

export abstract class BaseService {
    protected baseUrl: string;
    protected headers: Headers;
    protected options: RequestOptions;

    constructor() {
        this.headers = new Headers({ 'Content-Type': 'application/json' });
        this.options = new RequestOptions({ headers: this.headers });
    }
}