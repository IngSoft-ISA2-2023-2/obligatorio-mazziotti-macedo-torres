import { Component, OnInit } from '@angular/core';
import { cilCheckAlt, cilPencil } from '@coreui/icons';
import { CommonService } from '../../../services/CommonService';
import { Product } from 'src/app/interfaces/product';
import { ProductService } from 'src/app/services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-product-list',
  templateUrl: './edit-product-list.component.html',
  styleUrls: ['./edit-product-list.component.css'],
})
export class EditProductListComponent implements OnInit {
  products: Product[] = [];
  icons = { cilCheckAlt, cilPencil };
  targetItem: any = undefined;

  constructor(
    private commonService: CommonService,
    private productService: ProductService,
    private route: Router,
  ) { }

  ngOnInit(): void {
    this.getProductsByUser();
  }

  getProductsByUser() {
    this.productService.getProductsByUser().subscribe((p: any) => (this.products = p));
  }

  update(id: number): void {
    this.route.navigate(['employee/edit-product',
      { id: id }])
  }
}
