import axios, { AxiosResponse } from "axios";
import { Product } from "../models/product";

const sleep = (delay: number) => {
	return new Promise((resolve) => {
		setTimeout(resolve, delay);
	});
};

axios.defaults.baseURL = "https://localhost:44341/api";

axios.interceptors.response.use(async (response) => {
	try {
		await sleep(500);
		return response;
	} catch (error) {
		console.log(error);
		return await Promise.reject(error);
	}
});

const responseBody = <T>(response: AxiosResponse<T>) => response.data;


const requests = {
	get: <T>(url: string) => axios.get<T>(url).then(responseBody),
	post: <T>(url: string, body: {}) =>
		axios.post<T>(url, body).then(responseBody),
	put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
	delete: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const Products = {
	list: () => requests.get<Product[]>("./products"),
	details: (productid: string) =>
		requests.get<Product>(`/products/${productid}`),
	create: (product: Product) => requests.post<void>("/products", product),
	update: (product: Product) =>
		requests.put<void>(`/products/${product.productId}`, product),
	delete: (productid: string) =>
		requests.delete<void>(`/products/${productid}`),
};

const agent = {
	Products,
};

export default agent;
