import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Word } from '../models/word';
import settings from '../models/consts';

@Component({
  selector: 'app-exercise',
  templateUrl: './exercise.component.html',
  styleUrls: ['./exercise.component.css']
})
export class ExerciseComponent implements OnInit {

  words: Word[] = [];
  wordToTest: any;
  wordToTestText: string = "";
  wordToTestTranslate: string = "";
  wordText: string = "";
  showError: boolean = false;
  showSuccess: boolean = false;
  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.startTest();
  }

  getNextWord() {
    if(this.wordToTest) {
      this.wordToTest.learnt = true;
    }
    this.wordText = "";
    this.wordToTestTranslate = "";
    this.showSuccess = false;
    this.words = this.words.filter(word => !word.learnt);
    console.log(this.words);
    this.hideIncorrect();
    if(this.words.length == 0)
    {
      this.showSuccess = true;
      return;
    }
    let randIdx = this.getRandomInt();
    this.wordToTest = this.words[randIdx];
    this.wordToTestText = this.wordToTest.word;
    this.wordToTestTranslate = this.wordToTest.translate;
  }

  submitWord() {
    this.checkWord() ? this.getNextWord() : this.showIncorrect();
  }

  submitWithEnter(e: any)
  {
    if(e.KeyCode === 13)
    {
      e.preventDefault();
      this.submitWord();
    }
  }

  startOver () {
    this.startTest()
  }

  private showIncorrect() 
  {
    this.showError = true;
  }

  private hideIncorrect() 
  {
    this.showError = false;
  }

  private getRandomInt() : number {
    return Math.floor(Math.random() * this.words.length);
  }

  private checkWord() 
  {
    console.log(this.wordText);
    console.log(this.wordToTestText);
    return this.wordText.toLowerCase().trim() == this.wordToTestText.toLowerCase().trim();
  }

  private startTest()
  {
    this.httpClient
    .get<Word[]>(`${settings.domain}/exercises`)
    .subscribe(item => {
      this.words = item;
      this.getNextWord();
    });
  }

}
