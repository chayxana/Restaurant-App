import { Injectable } from '@angular/core';
import { BaseService } from "app/services/base.service";
import { Category } from "app/models/category";
import { Http } from '@angular/http';


@Injectable()
export class CategoryService extends BaseService<Category> {

  baseUrl(id?: string): string {
    return "http://localhost:4200/api/category/" + id;
  }

  constructor(http: Http) {
    super(http);
  }
}
