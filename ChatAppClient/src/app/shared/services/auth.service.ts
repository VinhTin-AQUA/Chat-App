import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  constructor(private http: HttpClient) { }

  register(form: FormData) {
    return this.http.post(environment.baseApi + "/Authentication/v1/register", form, {
      headers: {
        
      }
    });
  }

}
