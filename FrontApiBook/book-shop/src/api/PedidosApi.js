/** @format */

import axios from 'axios';
const url_api = 'https://localhost:7120/api/Order/';

export const getAllPedidosxUser = async (idUser) => {
	try {
		const response = await axios.get(url_api + 'GetAllOrdersByUser/' + idUser);
		console.log('RespuestaApiPedidos', response);
		return response.data;
	} catch (error) {
		throw error;
	}
};

export const getAllDetPedidosXpedido = async (idPedido) => {
	try {
		const response = await axios.get(
			url_api + 'GetAllDetailsOrdersbyOrder/' + idPedido,
		);
		return response.data;
	} catch (error) {
		throw error;
	}
};

export const CreateOrder = async (idUser) => {
	try {
		const response = await axios.post(
			url_api + 'CreateOrder/'+ idUser
		);
		return response.data;
	} catch (error) {
		throw error;
	}
};

export const PostDetPedidos = async (dePedido) => {
	try {
		const response = await axios.patch(
			url_api + 'CreateOrUpdateOrderDetail',
			dePedido,
			{
				headers: { 'Content-Type': 'application/json' },
			},
		);
		return response.data;
	} catch (error) {
		throw error;
	}
};
export const UpdateAmountDetailOrder = async (id, amount) => {
	try {
		const response = await axios.patch(
			`${url_api}UpdateAmountDetailOrder/${id}/${amount}`,
			{
				headers: { 'Content-Type': 'application/json' },
			},
		);
		return response.data;
	} catch (error) {
		throw error;
	}
};

export const GetOrderDetailsById = async (id) => {
	try {
		const response = await axios.get(url_api + 'GetOrderDetailsById/' + id);
		return response.data;
	} catch (error) {
		throw error;
	}
};
