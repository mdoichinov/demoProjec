import { LoadingmaskService } from './../services/loadingmask.service';
import { Component, OnInit } from '@angular/core';
import { catchError } from 'rxjs';
import { IPost } from '../models/post';
import { PostService } from '../services/post.service';

@Component({
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit {

  constructor(private _postService: PostService, private _loadingMaskService: LoadingmaskService) { }

  postArray?: IPost[];
  sortByAuthor?: string | null;
  sortByTitle?: string | null;

  private _showErrorMessage(message: string):void {
    alert(message);
  }

  ngOnInit(): void {
    this._loadingMaskService.isLoading = true;
    this.getPostArray();
  }

  getPostArray():void {
    this._postService.getPostList()
    .pipe(
      catchError(err => {
        this._showErrorMessage('Oops. Something went wrong!');
        throw new Error(err);
      })
    )
    .subscribe(
      (posts) => {
        this.postArray = posts;
        this._loadingMaskService.isLoading = false;
      }
    );
  }

  onAddFavoritePost(postId: number): void {
    this._loadingMaskService.isLoading = true;
    this._postService.setFavoritePost(postId).pipe(
      catchError(err => {
        this._showErrorMessage('Oops. Something went wrong!');
        throw new Error(err);
      })
    ).subscribe(() => this.getPostArray());
  }

  onRemoveFavoritePost(postId: number): void {
    this._loadingMaskService.isLoading = true;
    this._postService.deleteFavoritePost(postId).pipe(
      catchError(err => {
        this._showErrorMessage('Oops. Something went wrong!');
        throw new Error(err);
      })
    ).subscribe(() => this.getPostArray());
  }

  onDeletePost(postId: number) {
    if(confirm('Are you sure that you want to delete this post?')){
      this._loadingMaskService.isLoading = true;
      this._postService.deletePost(postId)
      .pipe(
        catchError(err => {
          this._showErrorMessage('Oops. Something went wrong!');
          throw new Error(err);
        })
      )
      .subscribe(() => this.getPostArray());
    }
  }

  onSortByAuthor(): void {
    this.sortByAuthor = this.sortByAuthor == 'asc' ? 'desc' : 'asc';
    this.sortByTitle = null;
    if(this.sortByAuthor == 'asc') {
      this.postArray?.sort((a,b) => (a.authorName > b.authorName) ? 1 : ((b.authorName > a.authorName) ? -1 : 0));
    }
    else {
      this.postArray?.sort((a,b) => (a.authorName < b.authorName) ? 1 : ((b.authorName < a.authorName) ? -1 : 0));
    }
  }

  onSortByTitle(): void {
    this.sortByTitle = this.sortByTitle == 'asc' ? 'desc' : 'asc';
    this.sortByAuthor = null;
    if(this.sortByTitle == 'asc') {
      this.postArray?.sort((a,b) => (a.postTitle > b.postTitle) ? 1 : ((b.postTitle > a.postTitle) ? -1 : 0));
    }
    else {
      this.postArray?.sort((a,b) => (a.postTitle < b.postTitle) ? 1 : ((b.postTitle < a.postTitle) ? -1 : 0));
    }
  }

  onResetData(): void {
    this._loadingMaskService.isLoading = true;
    this._postService.resetData()
    .pipe(
      catchError(err => {
        this._showErrorMessage('Oops. Something went wrong!');
        throw new Error(err);
      })
    )
    .subscribe(() => this.getPostArray());
  }
}
