import {RouterModule, Routes} from '@angular/router';
import {StoreComponent} from './store/store.component';
import {ProductDetails} from './product-details/product-details';
import {NgModule} from '@angular/core';
import {BasketComponent} from './basket/basket.component';

const routes: Routes = [
  {
    path: '',
    component: StoreComponent   //By default
  },
  {
    path: 'product/:id',
    component: ProductDetails
  },
  {
    path: 'basket',
    component: BasketComponent
  }
]
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StoreRoutingModule {

}
