import { Drink } from './Drink';

export interface DrinkType {
    id?: number;
    name: string;
    nrOfBrands: number;
    stock: number;
    profitMargin: number;
    drinks?: Drink[];
    [key: string]: any;
}