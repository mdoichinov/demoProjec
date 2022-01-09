import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { IPost } from '../models/post';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private httpClient: HttpClient) { }

  private _postsApiUrl: string = "http://localhost:24320/api/posts";
  private _singlePostApiUrl: string = "http://localhost:24320/api/posts/{postId}";
  private _favoritePostApiUrl: string = "http://localhost:24320/api/posts/{postId}/favorite";
  private _resetDataApiUrl: string = "http://localhost:24320/api/posts/createnewdataset";

  getPostList(): Observable<IPost[]> {
    return this.httpClient.get<IPost[]>(this._postsApiUrl);
  }

  getPostById(postId: number): Observable<IPost> {
    return this.httpClient.get<IPost>(this._singlePostApiUrl.replace("{postId}", postId.toString()));
  }

  deletePost(postId: number): Observable<null> {
    return this.httpClient.delete<null>(this._singlePostApiUrl.replace("{postId}", postId.toString()));
  }

  setFavoritePost(postId: number): Observable<null> {
    return this.httpClient.put<null>(this._favoritePostApiUrl.replace("{postId}", postId.toString()), null);
  }

  deleteFavoritePost(postId: number): Observable<null> {
    return this.httpClient.delete<null>(this._favoritePostApiUrl.replace("{postId}", postId.toString()));
  }

  resetData(): Observable<null>{
    return this.httpClient.post<null>(this._resetDataApiUrl, null);
  }
}
