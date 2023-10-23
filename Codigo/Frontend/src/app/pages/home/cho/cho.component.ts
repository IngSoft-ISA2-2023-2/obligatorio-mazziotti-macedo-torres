import { Component, OnInit } from '@angular/core';
import { cilThumbUp, cilCart, cilPlus, cilCompass } from '@coreui/icons';
import { IconSetService } from '@coreui/icons-angular';
import { Router } from '@angular/router';
import { PurchaseService } from '../../../services/purchase.service';
import { StorageManager } from '../../../utils/storage-manager';
import { PurchaseRequest, PurchaseRequestDrugsDetail, PurchaseRequestProductsDetail } from 'src/app/interfaces/purchase';
import { CommonService } from '../../../services/CommonService';
import { Drug } from 'src/app/interfaces/drug';
import { Product } from 'src/app/interfaces/product';

@Component({
  selector: 'app-cho',
  templateUrl: './cho.component.html',
  styleUrls: ['./cho.component.css']
})
export class ChoComponent implements OnInit {
  total: number = 0;
  email: string = "";
  cartDrugs: Drug[] = [];
  cartProducts: Product[] = [];

  constructor(
    public iconSet: IconSetService,
    private router: Router,
    private purchaseService: PurchaseService,
    private storageManager: StorageManager,
    private commonService: CommonService) {
    iconSet.icons = { cilThumbUp, cilCart, cilPlus, cilCompass };
  }

  ngOnInit(): void {
    this.updateCart();
    let _total = JSON.parse(this.storageManager.getData('totalDrugs')) + JSON.parse(this.storageManager.getData('totalProducts'));
    this.total = 0;
    if (_total){
      this.total = _total;
    }
  }

  finishPurchase(): void {
    let cartDrugs = JSON.parse(this.storageManager.getData('cartDrugs'));
    let cartProducts = JSON.parse(this.storageManager.getData('cartProducts'));

    let drugsDetails : PurchaseRequestDrugsDetail[] = [];
    let productsDetails : PurchaseRequestProductsDetail[] = [];

    for (const item of cartDrugs) {
      let drugsDetail = new PurchaseRequestDrugsDetail(item.code, item.quantity, item.pharmacy.id);
      drugsDetails.push(drugsDetail);
    }
    for (const item of cartProducts) {
      let productsDetail = new PurchaseRequestProductsDetail(item.code, item.quantity, item.pharmacy.id);
      productsDetails.push(productsDetail);
    }

    let now = new Date().toISOString();
    let purchaseRequest = new PurchaseRequest(this.email, now, drugsDetails, productsDetails);
    this.purchaseService.addPurchase(purchaseRequest)
    .subscribe(purchase => {
      if (purchase){
        console.log(purchase);
        this.commonService.updateToastData(
                  "Tracking code: " + purchase.trackingCode, 
                  "success", 
                  "Thank you for your purchase.");
        this.storageManager.removeData("cartDrugs");
        this.storageManager.removeData("cartProducts");          
        this.router.navigate(['/home']);
      }
    });
  }

  updateCart(): void {
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
  }
}
