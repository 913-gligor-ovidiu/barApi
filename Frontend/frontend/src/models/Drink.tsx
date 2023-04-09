import { DrinkType } from './DrinkType';

export interface Drink {
    id: number;
    name: string;
    price: number;
    quantity: number;
    abv: number;
    drinkType: DrinkType;
}