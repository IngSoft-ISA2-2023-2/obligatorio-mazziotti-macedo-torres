export interface Product {
    id: number;
    code: string;
    name: string;
    description: string;
    price: number;
    quantity: number;
    pharmacy: {
        id: number;
        name: string;
    };
}

export class ProductRequest {
    code: string;
    name: string;
    description: string;
    price: number;
    quantity: number;
    pharmacyName: string = "";

    constructor(code: string, name: string, description: string, price: number, quantity: number, pharmacyName: string) {
        this.code = code;
        this.name = name;
        this.description = description;
        this.price = price;
        this.quantity = quantity;
        this.pharmacyName = pharmacyName;
    }
}
