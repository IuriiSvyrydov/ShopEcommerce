export interface PaymentDto {
    id:string;
    orderId:string;
    amount:number;
    currency:string;
    status: 'Pending' | 'Processing' |'Paid' | 'Failed'| 'Refunded';
    createdAt: string;
    completedAt?: string;

}