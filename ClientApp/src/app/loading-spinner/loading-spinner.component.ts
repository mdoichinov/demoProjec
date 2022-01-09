import { LoadingmaskService } from './../services/loadingmask.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'dm-client-loading-spinner',
  templateUrl: './loading-spinner.component.html',
  styleUrls: ['./loading-spinner.component.css']
})
export class LoadingSpinnerComponent implements OnInit {

  constructor(private _loadingMask: LoadingmaskService) { }


  get isLoading(): boolean {
    return this._loadingMask.isLoading;
  }

  ngOnInit(): void {
  }

}
