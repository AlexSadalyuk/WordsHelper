import { Component, OnInit } from '@angular/core';
import { Translate } from '../models/translate';
import { Word } from '../models/word';

import { ApiServiceService } from '../api-service.service';

@Component({
  selector: 'app-words-list',
  templateUrl: './words-list.component.html',
  styleUrls: ['./words-list.component.css']
})
export class WordsListComponent implements OnInit 
{

  words: Word[] = [];
  wordText: string = "";
  translateText: string = "";
  wordToUpdate: any;
  updateMode: boolean = false;

  constructor(private _apiService: ApiServiceService) { }

  ngOnInit(): void 
  {
    this.initialWordsLoad();
  }

  submitPost() 
  {
    let translate = new Translate(this.translateText, this.words.length);
    let model = new Word(this.wordText,this.words.length, translate);

    this._apiService.postWord(model)
      .subscribe(()=>{
        this.initialWordsLoad();
        this.clearFields();
      });
  }

  setForUpdate(id: number)
  {
    this.wordToUpdate = this.words.find(word => word.id == id);
    this.wordText = this.wordToUpdate.text;
    this.translateText = this.wordToUpdate.translate.text;
    this.updateMode = true;
  }

  submitUpdate() 
  {
    if(this.wordToUpdate != null) 
    {
      this.wordToUpdate.text = this.wordText;
      this.wordToUpdate.translate.text = this.translateText;
      this._apiService.putWord(this.wordToUpdate).subscribe(() => {
        this.updateMode = false;
        this.clearFields();
        this.initialWordsLoad();
      });
    }
  }

  deleteWord(id: number)
  {
    this._apiService.deleteWord(id).subscribe(() => this.initialWordsLoad());
  }

  private initialWordsLoad() 
  {
    this._apiService
    .getWords()
    .subscribe(result => this.words = result);
  }
  private clearFields()
  {
    this.wordText = "";
    this.translateText = "";
  }
}
