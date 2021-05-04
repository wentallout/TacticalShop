import axios, { AxiosResponse } from "axios";
import { Product } from "../models/product";
import { Category } from "../models/category";
import * as Config from "../../config.js";
<<<<<<< HEAD
=======
import { User } from "../models/user";
>>>>>>> eea345c8ebc613772181eaab7d4d7aaa55fa8d55

const sleep = (delay: number) => {
	return new Promise((resolve) => {
		setTimeout(resolve, delay);
	});
};

axios.defaults.baseURL = `${Config.API_URL}/api`;
// axios.defaults.headers['Authorization'] = 'Bearer access_token';

axios.interceptors.request.use(function (config) {
	const token = localStorage.getItem("token");
	config.headers.Authorization = token ? `Bearer ${token}` : "";
	return config;
});

axios.interceptors.response.use(async (response) => {
	try {
		await sleep(100);
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

const Categories = {
	list: () => requests.get<Category[]>("./categories"),
	details: (categoryid: string) =>
		requests.get<Category>(`/categories/${categoryid}`),
	create: (category: Category) => requests.post<void>("/categories", category),
	update: (category: Category) =>
		requests.put<void>(`/categories/${category.categoryId}`, category),
	delete: (categoryid: string) =>
		requests.delete<void>(`/categories/${categoryid}`),
};

const Photos = {
	uploadPhoto: (file: Blob, productid) => {
		let formData = new FormData();
		formData.append("File", file);
		formData.append("productid", productid);
		return axios.post("/photos", formData, {
			headers: { "Content-type": "multipart/form-data" },
		});
	},
};

<<<<<<< HEAD
=======
const Users = {
	list: () => requests.get<User[]>("./users"),
	details: (id: string) => requests.get<User>(`/users/${id}`),
	create: (user: User) => requests.post<void>("/users", user),
	update: (user: User) => requests.put<void>(`/users/${user.id}`, user),
	delete: (id: string) => requests.delete<void>(`/users/${id}`),
};

>>>>>>> eea345c8ebc613772181eaab7d4d7aaa55fa8d55
const agent = {
	Products,
	Categories,
	Photos,
<<<<<<< HEAD
=======
	Users,
>>>>>>> eea345c8ebc613772181eaab7d4d7aaa55fa8d55
};

export default agent;
