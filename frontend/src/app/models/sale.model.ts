export interface SaleItem {
    id: string;
    product: string;
    quantity: number;
    unitPrice: number;
    discount: number;
    totalAmount: number;
    isCancelled: boolean;
  }
  
  export interface Sale {
    id: string;
    saleNumber: string;
    saleDate: string;
    customer: string;
    branch: string;
    totalAmount: number;
    isCancelled: boolean;
    items: SaleItem[];
  }
  