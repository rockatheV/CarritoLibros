/** @format */

import { useEffect, useState } from 'react';
import { getAllDetPedidosXpedido } from '../api/PedidosApi';
import {
	PostDetPedidos,
	GetOrderDetailsById,
	UpdateAmountDetailOrder,
} from '../api/PedidosApi';

const useDetPedidos = (pedidoId) => {
	const [detpedidos, setDetPedidos] = useState([]);
	const [detpedido, setDetPedido] = useState([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState(null);

	useEffect(() => {
		if (pedidoId) {
			fetchDetPedidos();
		}
	}, [pedidoId]);
	const fetchDetPedidos = async () => {
		try {
			const data = await getAllDetPedidosXpedido(pedidoId);
			console.log('pedidos', data);
			setDetPedidos(data.data);
		} catch (err) {
			setError(err);
		} finally {
			setLoading(false);
		}
	};
	const createOrEditOrderDetail = async (ordenDet) => {
		try {
			const data = await PostDetPedidos(ordenDet);
			fetchDetPedidos();
		} catch (err) {
		} finally {
			setLoading(false);
		}
	};
	const UpdateAmountDetail = async (id, amount) => {
		try {
			const data = await UpdateAmountDetailOrder(id, amount);
			fetchDetPedidos();
		} catch (err) {
		} finally {
			setLoading(false);
		}
	};
	const GetDetailsById = async (id) => {
		try {
			const data = await GetOrderDetailsById(id);
			setDetPedido(data.data);
			return data.data;
		} catch (err) {
		} finally {
			setLoading(false);
		}
	};

	return {
		detpedidos,
		detpedido,
		loading,
		error,
		createOrEditOrderDetail,
		UpdateAmountDetail,
		GetDetailsById,
	};
};

export default useDetPedidos;
