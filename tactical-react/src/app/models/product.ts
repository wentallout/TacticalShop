export interface Product {
	productId: string;
	productName: string;
	productPrice: string;
	productDescription: string;
	productImageName: string;
	productQuantity: string;
	categoryId: string;
	brandId: string;
	categoryName?: string;
	brandName?: string;
	createdDate?: string;
	updatedDate?: string;
	starRating?: string;
}
