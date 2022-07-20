import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import settings from './models/consts';
import { Word } from './models/word';

@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {

  private url: string = "words";

  constructor(private _httpClient:HttpClient) { }

  public getWords()
  {
    return this._httpClient.get<Word[]>(`${settings.domain}/${this.url}`);
  }

  public getWord(id: number)
  {
    return this._httpClient.get<Word>(`${settings.domain}/${this.url}/${id}`);
  }
  
  public postWord(word: Word)
  {
    return this._httpClient.post<Word>(`${settings.domain}/${this.url}`, word);
  }

  public putWord(word: Word)
  {
    return this._httpClient.put<Word>(`${settings.domain}/${this.url}`, word);
  }

  public deleteWord(id: number)
  {
    return this._httpClient.delete(`${settings.domain}/${this.url}/${id}`);
  }
}
