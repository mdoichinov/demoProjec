import { IComment } from './../models/comment';
import { CommentService } from './../services/comment.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { catchError, forkJoin } from 'rxjs';
import { IPost } from '../models/post';
import { PostService } from '../services/post.service';
import { LoadingmaskService } from '../services/loadingmask.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { IAddComment } from '../models/add-comment';

@Component({
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.css']
})
export class PostDetailComponent implements OnInit {

  post?: IPost;
  commentArray?: IComment[];

  addCommentForm: FormGroup | undefined;

  constructor(
    private _postService: PostService,
    private _loadingMaskService: LoadingmaskService,
    private _commentService: CommentService,
    private _route: ActivatedRoute) { }

    authorEmail!: FormControl;
    commentMessage!: FormControl;
    validateForm: boolean = false;

  private _showErrorMessage(message: string, critical: boolean = false):void {
    alert(message);
    if(!critical) {
      this._loadingMaskService.isLoading = false;
    }
  }

  private _loadComments(): void{
    this._commentService.getCommentList(<number>this.post?.id)
      .pipe(
        catchError(err => {
          this._showErrorMessage('Oops. Something went wrong!', true);
          throw new Error(err);
        }))
        .subscribe(commentArray => {
          this.commentArray = commentArray;
          this._loadingMaskService.isLoading = false;
          this.validateForm = false;
          this.authorEmail.setValue("");
          this.commentMessage.setValue("");
        });
  }

  ngOnInit(): void {
    this._loadingMaskService.isLoading = true;
    this.authorEmail = new FormControl(null, [Validators.required, Validators.email]);
    this.commentMessage = new FormControl(null, [Validators.required]);

    this._route.params.subscribe(params => {
      forkJoin({
        post: this._postService.getPostById(params['postId']),
        commentArray: this._commentService.getCommentList(params['postId'])
      })
      .pipe(
        catchError(err => {
          this._showErrorMessage('Oops. Something went wrong!');
          throw new Error(err);
        })
      )
      .subscribe(
        ({post, commentArray}) => {
          this.post = post;
          this.commentArray = commentArray;
          this._loadingMaskService.isLoading = false;
        }
      );

    });
  }

  onDeleteComment(commentId: number): void {
    if(!confirm("Are you sure that you want to delete a comment?")){
      return;
    }
    this._loadingMaskService.isLoading = true;
    this._commentService.deleteComment(<number>this.post?.id, commentId).pipe(
      catchError(err => {
        this._showErrorMessage('Oops. Something went wrong!');
        throw new Error(err);
      })
    ).subscribe(()=> this._loadComments());
  }

  onSendPost(): void {
    this.validateForm = true;
    if(!this.authorEmail.valid || !this.commentMessage.valid){
      return;
    }
    this._loadingMaskService.isLoading = true;

    const newComment: IAddComment = {
      authorEmail: this.authorEmail.value,
      comment: this.commentMessage.value
    }
    this._commentService.postComment(<number>this.post?.id, newComment).pipe(
      catchError(err => {
        this._showErrorMessage('Oops. Something went wrong!');
        throw new Error(err);
      })
    ).subscribe(()=> this._loadComments());

  }

}
