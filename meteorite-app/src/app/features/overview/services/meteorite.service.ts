import { Injectable } from '@angular/core';
import { MeteoriteGrouped } from '../models/meteorite-grouped';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MeteoriteGroupedFilter } from '../models/meteorite-grouped-filter';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { MeteoritesDictionaries } from '../models/meteorites-dictionaries';

@Injectable({
  providedIn: 'root'
})
export class MeteoriteService {

  private _httpClient : HttpClient;

  constructor(http: HttpClient) {
    this._httpClient = http;
  }

  getMeteoritesGroupedData(filter?: MeteoriteGroupedFilter): Observable<MeteoriteGrouped[]>
  {
    return this._httpClient.get<MeteoriteGrouped[]>(`${environment.apiUrl}/${environment.meteoriteApiUri}`, { 
      params: {
        fromYear: filter?.fromYear,
        toYear: filter?.toYear,
        recClass: filter?.recClass,
        name: filter?.name
    }});
  }

  getMeteoritesDictionaries(): Observable<MeteoritesDictionaries> {
    return this._httpClient.get<MeteoritesDictionaries>(`${environment.apiUrl}/${environment.meteoriteApiUri}/dictionaries`);
  } 
}
