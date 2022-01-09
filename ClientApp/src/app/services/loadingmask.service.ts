import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoadingmaskService {

  private _isLoading: boolean = false;
  public get isLoading() {
    return this._isLoading;
  };
  public set isLoading(value: boolean) {
    this._isLoading = value;
  };
  constructor() { }
}
