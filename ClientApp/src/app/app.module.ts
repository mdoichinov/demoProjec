import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { PostListComponent } from './post-list/post-list.component';
import { PostDetailComponent } from './post-detail/post-detail.component';
import { HttpClientModule } from '@angular/common/http';
import { HoverClassDirective } from './hover-class.directive';
import { LoadingSpinnerComponent } from './loading-spinner/loading-spinner.component';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    PostListComponent,
    PostDetailComponent,
    HoverClassDirective,
    LoadingSpinnerComponent,
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
