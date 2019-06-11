import { Injectable } from '@angular/core';
import { Food } from 'app/models/food';
import { BaseService } from 'app/services/base.service';
import { HttpRequest, HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';

@Injectable()
export class FoodService extends BaseService<Food> {
  private _baseUrl: string | null = null;

  constructor(http: HttpClient) {
    super(http);
  }

  get BaseUrl(): string {
    if (this._baseUrl === null) {
      this._baseUrl = `${environment.apiUrl}/api/v1/foods`;
    }
    return this._baseUrl;
  }

  createFood(food: Food, file: File): any {
    return new HttpRequest('POST', this.BaseUrl, file, {
      reportProgress: true,
    });
  }

  uploadImage(picture: File, foodId: string) {
    return new Promise((resolve, reject) => {
      const xhr: XMLHttpRequest = new XMLHttpRequest();
      xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
          if (xhr.status === 200) {
            resolve(true);
          } else {
            reject(false);
          }
        }
      };

      xhr.open('POST', this.BaseUrl + '/UploadFile', true);
      const formData = new FormData();
      formData.append('file', picture, picture.name);
      formData.append('foodId', foodId);
      xhr.send(formData);
    });
  }
}
