export interface FoodDto {
    id:              string;
    name:            string;
    description:     string;
    pictures:        Picture[];
    price:           number;
    category:        Category;
    categoryId:      string;
    currency:        string;
    deletedPictures: null;
}

export interface Category {
    id:    string;
    name:  string;
    color: string;
}

export interface Picture {
    id:       string;
    filePath: string;
}