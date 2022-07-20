import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule, Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http'

import { AppComponent } from './app.component';
import { WordsListComponent } from './words-list/words-list.component';
import { ExerciseComponent } from './exercise/exercise.component';
import { FormsModule } from '@angular/forms';
import { ApiServiceService } from './api-service.service';

const routes: Routes = [
  { path: 'words-list', component: WordsListComponent}
 ,{ path: 'exercises', component: ExerciseComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    WordsListComponent,
    ExerciseComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes)
  ],
  exports: [],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
