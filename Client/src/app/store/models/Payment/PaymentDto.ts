import {PaymentStatus} from './PaymentStatus';

export interface PaymentDto {
    id:string;
    orderId:string;
    amount:number;
    currency:string;
    status: PaymentStatus;
    createdAt: string;
    completedAt?: string;

}
