import { Injectable } from '@angular/core';
import { BaseService } from 'app/services/base.service';
import { Category } from 'app/models/category';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { AuthService } from './auth.service';

@Injectable()
export class CategoryService extends BaseService<Category> {
  constructor(http: HttpClient) {
    super(http);
  }

  private _baseUrl: string | null = null;

  get BaseUrl(): string {
    if (this._baseUrl === null) {
      this._baseUrl = `${environment.apiUrl}/api/v1/categories`;
    }
    return this._baseUrl;
  }
}
