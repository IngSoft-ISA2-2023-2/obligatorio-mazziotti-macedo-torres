import { Component, OnInit } from '@angular/core';
import { cilCart, cilPlus, cilCompass } from '@coreui/icons';
import { IconSetService } from '@coreui/icons-angular';
import { ActivatedRoute } from '@angular/router';
import { StorageManager } from '../../../utils/storage-manager';
import { Router } from '@angular/router';
import { CommonService } from '../../../services/CommonService';
import { Product } from 'src/app/interfaces/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-detail-product',
  templateUrl: './detail-product.component.html',
  styleUrls: ['./detail-product.component.css'],
})
export class DetailProductComponent implements OnInit {
  product: Product | undefined;
  quantity: number = 1;
  cart: any[] = [];

  constructor(
    private route: ActivatedRoute,
    public iconSet: IconSetService,
    private productService: ProductService,
    private storageManager: StorageManager,
    private router: Router,
    private commonService: CommonService
  ) {
    iconSet.icons = { cilCart, cilPlus, cilCompass };
  }

  ngOnInit(): void {
    this.getProduct();
    this.storageManager.saveData('totalProducts', JSON.stringify(0));
  }

  getProduct(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    this.productService.getProduct(id).subscribe((product) => (this.product = product));
  }

  addToCart(product: Product) {
    if (product) {
      this.cart = JSON.parse(this.storageManager.getData('cartProducts'));
      if (!this.cart) {
        this.cart = [];
        this.storageManager.saveData('cartProducts', JSON.stringify(this.cart));
      }

      let exist: boolean = false;
      for (let item of this.cart) {
        if (item.code === product.code && item.id === product.id) {
          item.quantity += this.quantity;
          exist = true;
          break;
        }
      }
      if (!exist) {
        product.quantity = this.quantity;
        this.cart.push(product);
      }
      this.storageManager.saveData('cartProducts', JSON.stringify(this.cart));
    }
    this.updateHeader(this.cart.length);
    this.router.navigate(['/home/cart']);
  }

  updateHeader(value: number) {
    this.commonService.updateHeaderData(value);
  }

}
