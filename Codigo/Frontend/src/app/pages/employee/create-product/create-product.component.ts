import { Component, OnInit } from '@angular/core';
import { cilBarcode, cilPencil, cilAlignCenter, cilDollar } from '@coreui/icons';
import { AbstractControl, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Pharmacy } from '../../../interfaces/pharmacy';
import { UnitMeasure } from '../../../interfaces/unitmeasure';
import { Presentation } from '../../../interfaces/presentation';
import { CommonService } from '../../../services/CommonService';
import { ProductRequest } from 'src/app/interfaces/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css'],
})
export class CreateProductComponent implements OnInit {
  form: FormGroup | any;
  pharmacys: Pharmacy[] = [];
  unitMeasure: UnitMeasure[] = [];
  presentation: Presentation[] = [];

  icons = { cilBarcode, cilPencil, cilAlignCenter, cilDollar };

  constructor(
    private commonService: CommonService,
    private productService: ProductService,
    private fb: FormBuilder
  ) {

    this.form = this.fb.group({
      code: new FormControl(),
      name: new FormControl(),
      description: new FormControl(),
      price: new FormControl(),
    });
  }

  ngOnInit(): void {

  }

  get code(): AbstractControl {
    return this.form.controls.code;
  }

  get name(): AbstractControl {
    return this.form.controls.name;
  }
  get description(): AbstractControl {
    return this.form.controls.description;
  }

  get price(): AbstractControl {
    return this.form.controls.price;
  }

  createProduct(): void {
    let code = this.code.value ? this.code.value : "";
    let name = this.name.value ? this.name.value : "";
    let description = this.description.value ? this.description.value : "";
    let price = this.price.value ? this.price.value : 0;

    let productRequest = new ProductRequest(code, name, description, price, 0, "");
    this.productService.createProduct(productRequest).subscribe((product) => {
      this.form.reset();

      if (product) {
        this.commonService.updateToastData(
          `Success creating "${product.code} - ${product.name}"`,
          'success',
          'Product created.'
        );
      }
    });

  }
}
