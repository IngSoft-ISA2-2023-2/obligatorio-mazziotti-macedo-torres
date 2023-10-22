import { Component, OnInit } from '@angular/core';
import { cilAlignCenter, cilBarcode, cilCheckAlt, cilDollar, cilPencil } from '@coreui/icons';
import { CommonService } from '../../../services/CommonService';
import { Product, ProductRequest } from 'src/app/interfaces/product';
import { ProductService } from 'src/app/services/product.service';
import { AbstractControl, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css'],
})
export class EditProductComponent implements OnInit {
  form: FormGroup | any;
  product: Product | any;
  icons = { cilBarcode, cilPencil, cilAlignCenter, cilDollar };
  visible = false;
  modalTitle = '';
  modalMessage = '';

  constructor(
    private commonService: CommonService,
    private productService: ProductService,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) {
    this.form = fb.group({
      code: new FormControl(),
      name: new FormControl(),
      description: new FormControl(),
      price: 0
    });
    this.product = null;
  }

  ngOnInit(): void {
    let id = this.route.snapshot.paramMap.get('id');
    this.getProductById(id);
  }

  getProductById(id: any): void {
    this.productService
      .getProductById(id)
      .subscribe((product) => {
        this.product = product;

        this.setDefaultUserCode(this.product.code);
        this.setDefaultUserName(this.product.name);
        this.setDefaultUserDescription(this.product.description);
        this.setDefaultUserPrice(this.product.price);
      });
  }

  setDefaultUserCode(code: string): void {
    this.form.controls.code.setValue(code);
  }
  setDefaultUserName(name: string): void {
    this.form.controls.name.setValue(name);
  }
  setDefaultUserDescription(description: string): void {
    this.form.controls.description.setValue(description);
  }
  setDefaultUserPrice(price: string): void {
    this.form.controls.price.setValue(price);
  }

  get product_code(): AbstractControl {
    return this.form.controls.code;
  }

  get product_name(): AbstractControl {
    return this.form.controls.name;
  }
  get product_description(): AbstractControl {
    return this.form.controls.description;
  }

  get product_price(): AbstractControl {
    return this.form.controls.price;
  }

  editProduct(): void {
    let code = this.product_code.value ? this.product_code.value : "";
    let name = this.product_name.value ? this.product_name.value : "";
    let description = this.product_description.value ? this.product_description.value : "";
    let price = this.product_price.value ? this.product_price.value : 0;
    let id = this.product.id;

    let productRequest = new ProductRequest(code, name, description, price, 0, "");
    this.productService.editProduct(id, productRequest).subscribe((product) => {
      if (product) {
        this.commonService.updateToastData("Success editing product", 'success', 'Product edited.');
      }
    });
  }
}
