/** @format */

import axios from 'axios';
const url_api = 'https://localhost:7120/api/Books/';

export const getAllBooks = async () => {
	try {
		const response = await axios.get(url_api + 'GetAllBooks');
		return response.data;
	} catch (error) {
		throw error;
	}
};
export const getBooksById = async (id) => {
	try {
		const response = await axios.get(url_api + 'GetByIdBook/' + id);
		return response.data;
	} catch (error) {
		throw error;
	}
};

export const PostBooks = async (book) => {
	try {
		const response = await axios.post(url_api + 'CreateBook', book, {
			headers: { 'Content-Type': 'application/json' },
		});
		return response.data;
	} catch (error) {
		throw error;
	}
};

export const EditBooks = async (id, book) => {
	try {
		const response = await axios.put(url_api + 'EditBook/' + id, book, {
			headers: { 'Content-Type': 'application/json' },
		});
		return response.data;
	} catch (error) {
		throw error;
	}
};
export const DeleteBooksA = async (id) => {
	try {
		const response = await axios.delete(url_api + 'libros/' + id, {
			headers: { 'Content-Type': 'application/json' },
		});
		return response.data;
	} catch (error) {
		throw error;
	}
};
