import { PostDetailComponent } from './post-detail/post-detail.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PostListComponent } from './post-list/post-list.component';

@NgModule({
  imports: [
    RouterModule.forRoot([
      { path: 'posts', component: PostListComponent},
      { path: 'post/:postId', component: PostDetailComponent},
      { path: '', redirectTo: 'posts', pathMatch: 'full' },
      { path: '**', component: PostListComponent }
    ], { useHash: true })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
