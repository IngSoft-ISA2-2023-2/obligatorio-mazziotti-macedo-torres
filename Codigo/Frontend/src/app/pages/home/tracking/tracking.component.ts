import { Component, OnInit } from '@angular/core';
import { cilCart, cilPlus, cilCompass } from '@coreui/icons';
import { IconSetService } from '@coreui/icons-angular';
import { Drug } from 'src/app/interfaces/drug';
import { PurchaseResponse } from '../../../interfaces/purchase';
import { PurchaseService } from '../../../services/purchase.service';
import { StorageManager } from '../../../utils/storage-manager';
import { CommonService } from '../../../services/CommonService';
import { Product } from 'src/app/interfaces/product';

@Component({
  selector: 'app-tracking',
  templateUrl: './tracking.component.html',
  styleUrls: ['./tracking.component.css']
})
export class TrackingComponent implements OnInit {
  
  purchase: PurchaseResponse | undefined;
  code: string = "";
  tracking: string[] = [];
  cartDrugs: Drug[] = [];
  cartProducts: Product[] = [];

  constructor(
    public iconSet: IconSetService,
    private purchaseService: PurchaseService,
    private storageManager: StorageManager,
    private commonService: CommonService) {
    iconSet.icons = { cilCart, cilPlus, cilCompass };
  }

  ngOnInit(): void {
    let track = JSON.parse(this.storageManager.getData('tracking'));
    if (!track){
      this.tracking = [];
    }else{
      this.tracking = track;
    }

    this.updateCart();
  }

  getPurchaseByTrackingCode(){
    this.purchaseService.getPurchaseByTrackingCode(this.code)
    .subscribe((p: PurchaseResponse) => this.purchase = p);

    if (this.tracking.length > 4){
      this.tracking.shift();
    }
    this.tracking.push(this.code);
    this.storageManager.saveData('tracking', JSON.stringify(this.tracking));
  }

  getColor(status: string) {
    if (status === 'Approved'){
      return "green";
    }else if (status === 'Rejected'){
      return "red";
    }
    // Pending
    return "orange";
  }

  updateCart() : void{
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
    this.commonService.updateHeaderData(this.cartDrugs.length + this.cartProducts.length);
  }
}