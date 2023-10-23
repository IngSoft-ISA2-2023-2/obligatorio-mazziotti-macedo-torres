import { BehaviorSubject } from 'rxjs';

export class CommonService {
  private headerDataUpdateSource = new BehaviorSubject<any>(0);
  onHeaderDataUpdate = this.headerDataUpdateSource.asObservable();

  private toastDataUpdateSource = new BehaviorSubject<any>({});
  onToastDataUpdate = this.toastDataUpdateSource.asObservable();

  private searchDrugsDataUpdateSource = new BehaviorSubject<any>([]);
  onSearchDrugsDataUpdate = this.searchDrugsDataUpdateSource.asObservable();

  private searchProductsDataUpdateSource = new BehaviorSubject<any>([]);
  onSearchProductsDataUpdate = this.searchProductsDataUpdateSource.asObservable();

  updateHeaderData(message: any) {
    this.headerDataUpdateSource.next(message);
  }

  updateToastData(message: any, type: any, title: any) {
    this.toastDataUpdateSource.next({message, type, title});
  }

  updateSearchDrugsData(data: any) {
    this.searchDrugsDataUpdateSource.next(data);
  }

  updateSearchProductsData(data: any) {
    this.searchProductsDataUpdateSource.next(data);
  }
}
