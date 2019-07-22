import { Injectable } from '@angular/core';
import { Food } from 'app/models/food';
import { BaseService } from 'app/services/base.service';
import { HttpRequest, HttpClient, HttpEventType, HttpResponse, HttpHeaders } from '@angular/common/http';
import { environment } from 'environments/environment';
import { AuthService } from './auth.service';
import { Subject, Observable } from 'rxjs';

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

  uploadImage(files: File[], foodId: string, token: string): Observable<number> {

    const formData: FormData = new FormData();
    formData.append('foodId', foodId);

    files.forEach(file => {
      formData.append('files', file, file.name);
    });

    const req = new HttpRequest('POST', this.BaseUrl + '/UploadFoodImage', formData, {
      reportProgress: true,
      headers: new HttpHeaders({
        'Authorization': token
      })
    });

    // create a new progress-subject for every file
    const progress = new Subject<number>();

    const startTime = new Date().getTime();
    this.http.request(req).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress) {
        // calculate the progress percentage

        const percentDone = Math.round((100 * event.loaded) / event.total);
        // pass the percentage into the progress-stream
        progress.next(percentDone);
      } else if (event instanceof HttpResponse) {
        // Close the progress-stream if we get an answer form the API
        // The upload is complete
        progress.complete();
      }
    });

    return progress.asObservable();
  }
}
