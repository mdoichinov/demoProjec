import { IAddComment } from './../models/add-comment';
import { IComment } from './../models/comment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private httpClient: HttpClient) { }

  private _commentsForPostApiUrl: string = "http://localhost:24320/api/posts/{postId}/comments";
  private _deleteCommentForPostApiUrl: string = "http://localhost:24320/api/posts/{postId}/comments/{commentId}";

  getCommentList(postId: number): Observable<IComment[]> {
    return this.httpClient.get<IComment[]>(this._commentsForPostApiUrl.replace("{postId}", postId.toString()));
  }

  deleteComment(postId: number, commentId: number): Observable<null>{
    return this.httpClient.delete<null>(this._deleteCommentForPostApiUrl
      .replace("{postId}", postId.toString())
      .replace("{commentId}", commentId.toString()));
  }

  postComment(postId: number, comment: IAddComment): Observable<null> {
    return this.httpClient.post<null>(this._commentsForPostApiUrl.replace("{postId}", postId.toString()),
    comment);
  }
}
