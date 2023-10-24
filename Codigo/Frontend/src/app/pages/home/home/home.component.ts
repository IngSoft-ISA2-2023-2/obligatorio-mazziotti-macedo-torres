import { Component, OnInit } from '@angular/core';
import { cilCart, cilPlus, cilCompass } from '@coreui/icons';
import { IconSetService } from '@coreui/icons-angular';
import { Drug } from '../../../interfaces/drug';
import { DrugService } from '../../../services/drug.service';
import { CommonService } from '../../../services/CommonService';
import { StorageManager } from 'src/app/utils/storage-manager';
import { Product } from 'src/app/interfaces/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  drugs: Drug[] = [];
  products: Product[] = [];
  cartDrugs: Drug[] = [];
  cartProducts: Product[] = [];

  constructor(
    public iconSet: IconSetService,
    private drugService: DrugService,
    private productService: ProductService,
    private commonService: CommonService,
    private storageManager: StorageManager) {
    iconSet.icons = { cilCart, cilPlus, cilCompass };

    this.commonService.onSearchDrugsDataUpdate.subscribe((data: any) => {
      this.drugs = data;
    });

    this.commonService.onSearchProductsDataUpdate.subscribe((data: any) => {
      this.products = data;
    });
  }

  ngOnInit(): void {
    this.updateCartDrugs();
    this.updateCartProducts();
    this.getDrugs();
    this.getProducts();
    this.storageManager.saveData('totalDrugs', JSON.stringify(0));
    this.storageManager.saveData('totalProducts', JSON.stringify(0));
  }

  getDrugs(): void {
    this.drugService.getDrugs()
      .subscribe(drugs => this.drugs = drugs);
  }

  getProducts(): void {
    this.productService.getProducts()
      .subscribe(products => this.products = products);
  }

  updateCartDrugs(): void {
    this.cartDrugs = JSON.parse(this.storageManager.getData('cartDrugs'));
    if (!this.cartDrugs) {
      this.cartDrugs = [];
      this.storageManager.saveData('cartDrugs', JSON.stringify(this.cartDrugs));
    }
    this.commonService.updateHeaderData(this.cartDrugs.length);
  }

  updateCartProducts(): void {
    this.cartProducts = JSON.parse(this.storageManager.getData('cartProducts'));
    if (!this.cartProducts) {
      this.cartProducts = [];
      this.storageManager.saveData('cartProducts', JSON.stringify(this.cartProducts));
    }
    this.commonService.updateHeaderData(this.cartProducts.length);
  }
}
