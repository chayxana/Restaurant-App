export interface IFoodDto {
    id:              string;
    name:            string;
    description:     string;
    pictures:        IPicture[];
    price:           number;
    category:        ICategory;
    categoryId:      string;
    currency:        string;
}

export interface ICategory {
    id:    string;
    name:  string;
    color: string;
}

export interface IPicture {
    id:       string;
    filePath: string;
}