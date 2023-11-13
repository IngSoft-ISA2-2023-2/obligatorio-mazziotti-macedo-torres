import { Component, OnInit, Input } from '@angular/core';
import { Pharmacy } from '../../interfaces/pharmacy';
import { CommonService } from '../../services/CommonService';
import { PharmacyService } from '../../services/pharmacy.service';
import { DrugService } from '../../services/drug.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-custom-header',
  templateUrl: './custom-header.component.html',
  styleUrls: ['./custom-header.component.css'],
})
export class CustomHeaderComponent implements OnInit {
  pharmacys: Pharmacy[] = [];
  badge: number = 0;
  searchValue: string = "";
  pharmacyId: string = "0";

  @Input() icons: boolean = true;
  @Input() select: boolean = true;
  @Input() search: boolean = true;

  constructor(
    private commonService: CommonService,
    private pharmacyService: PharmacyService,
    private drugService: DrugService,
    private productsService: ProductService
  ) {
    var self = this;
    this.commonService.onHeaderDataUpdate.subscribe((data: any) => {
      self.badge = data;
    });
  }

  ngOnInit(): void {
    this.getPharmacys();
  }

  getPharmacys(): void {
    this.pharmacyService
      .getPharmacys()
      .subscribe((pharmacys) => (this.pharmacys = pharmacys));
  }

  onChangeSelect(id: any): void {
    this.pharmacyId = id;
    this.getDrugsFilter(this.pharmacyId, this.searchValue);
    this.getProductsFilter(this.pharmacyId, this.searchValue);
  }

  onSearch() {
    this.getDrugsFilter(this.pharmacyId, this.searchValue);
    this.getProductsFilter(this.pharmacyId, this.searchValue);
  }

  getDrugsFilter(pharmacyId: string, pharmacyName: string): void {
    this.drugService.getDrugsFilter(pharmacyId, pharmacyName)
      .subscribe(drugs => {
        this.commonService.updateSearchDrugsData(drugs);
      });
  }

  getProductsFilter(pharmacyId: string, pharmacyName: string): void {
    this.productsService.getProductsFilter(pharmacyId, pharmacyName)
      .subscribe(products => {
        this.commonService.updateSearchProductsData(products);
      });
  }
}
