export class PurchaseRequest {
  buyerEmail: string = "";
  purchaseDate: string = "";
  drugsDetails: PurchaseRequestDrugsDetail[] = [];
  productsDetails: PurchaseRequestProductsDetail[] = [];

  constructor(buyerEmail: string,
    purchaseDate: string,
    drugsDetails: PurchaseRequestDrugsDetail[],
    productsDetails: PurchaseRequestProductsDetail[]) {
    this.buyerEmail = buyerEmail;
    this.purchaseDate = purchaseDate;
    this.drugsDetails = drugsDetails;
    this.productsDetails = productsDetails;
  }
}

export class PurchaseRequestDrugsDetail {
  code: string = "";
  quantity: number = 1;
  pharmacyId: number = 1;

  constructor(code: string,
    quantity: number,
    pharmacyId: number) {
    this.code = code;
    this.quantity = quantity;
    this.pharmacyId = pharmacyId;
  }
}

export class PurchaseRequestProductsDetail {
  code: string = "";
  quantity: number = 1;
  pharmacyId: number = 1;

  constructor(code: string,
    quantity: number,
    pharmacyId: number) {
    this.code = code;
    this.quantity = quantity;
    this.pharmacyId = pharmacyId;
  }
}

export interface PurchaseResponse {
  id: number;
  buyerEmail: string;
  purchaseDate: string;
  trackingCode: string;
  totalAmount: number;
  drugsDetails: PurchaseDrugsDetailModelResponse[];
  productsDetails: PurchaseProductsDetailModelResponse[];
}

export interface PurchaseDrugsDetailModelResponse {
  id: number;
  code: string;
  name: string;
  quantity: number;
  price: number;
  pharmacyId: number;
  pharmacyName: string;
  status: string;
}

export interface PurchaseProductsDetailModelResponse {
  id: number;
  code: string;
  name: string;
  quantity: number;
  price: number;
  pharmacyId: number;
  pharmacyName: string;
  status: string;
}

export interface PurchaseModelResponseStatus {
  purchaseId: number;
  purchaseDetailId: number;
  drugCode: string;
  drugName: string;
  quantity: number;
  price: number;
  pharmacyId: number;
  pharmacyName: string;
  status: string;
}