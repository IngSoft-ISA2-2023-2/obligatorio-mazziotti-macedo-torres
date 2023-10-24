import { Component, OnInit } from '@angular/core';
import { cilCart, cilPlus, cilCompass, cilCheckCircle, cilTrash } from '@coreui/icons';
import { IconSetService } from '@coreui/icons-angular';
import { Drug } from 'src/app/interfaces/drug';
import { StorageManager } from '../../../utils/storage-manager';
import { Router } from '@angular/router';
import { CommonService } from '../../../services/CommonService';
import { Product } from 'src/app/interfaces/product';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cartDrugs: Drug[] = [];
  cartProducts: Product[] = [];
  totalDrugs: number = 0;
  totalProducts: number = 0;

  constructor(
    public iconSet: IconSetService,
    private storageManager: StorageManager,
    private router: Router,
    private commonService: CommonService) {
    iconSet.icons = { cilCart, cilPlus, cilCompass, cilCheckCircle, cilTrash };
  }

  ngOnInit(): void {
    this.cartDrugs = JSON.parse(this.storageManager.getData('cartDrugs'));
    this.cartProducts = JSON.parse(this.storageManager.getData('cartProducts'));
    if (!this.cartDrugs) {
      this.cartDrugs = [];
      this.storageManager.saveData('cartDrugs', JSON.stringify(this.cartDrugs));
    }
    if (!this.cartProducts) {
      this.cartProducts = [];
      this.storageManager.saveData('cartProducts', JSON.stringify(this.cartProducts));
    }
    this.storageManager.saveData('totalDrugs', JSON.stringify(0));
    this.storageManager.saveData('totalProducts', JSON.stringify(0));
    this.updateTotalDrugs();
    this.updateTotalProducts();
  }

  deleteDrug(index: number){
    this.cartDrugs = JSON.parse(this.storageManager.getData('cartDrugs'));
    this.cartDrugs.splice(index, 1);
    this.storageManager.saveData('cartDrugs', JSON.stringify(this.cartDrugs));
    this.updateTotalDrugs();
    this.updateHeader(this.cartDrugs.length);
  }

  deleteProduct(index: number){
    this.cartProducts = JSON.parse(this.storageManager.getData('cartProducts'));
    this.cartProducts.splice(index, 1);
    this.storageManager.saveData('cartProducts', JSON.stringify(this.cartProducts));
    this.updateTotalProducts();
    this.updateHeader(this.cartProducts.length);
  }

  updateTotalDrugs(){
    this.totalDrugs = 0;
    this.cartDrugs = JSON.parse(this.storageManager.getData('cartDrugs'));
    for(let item of this.cartDrugs){
      this.totalDrugs += (item.price * item.quantity);
    }
  }

  updateTotalProducts(){
    this.totalProducts = 0;
    this.cartProducts = JSON.parse(this.storageManager.getData('cartProducts'));
    for(let item of this.cartProducts){
      this.totalProducts += (item.price * item.quantity);
    }
  }

  updateHeader(value: number){
    this.commonService.updateHeaderData(value);
   }

  goToCho(){
    this.storageManager.saveData('totalDrugs', JSON.stringify(this.totalDrugs));
    this.storageManager.saveData('totalProducts', JSON.stringify(this.totalProducts));
    this.router.navigate(['/home/cart/cho']);
  }
  
}
