/** @format */

import { useEffect, useState } from 'react';
import { getAllPedidosxUser } from '../api/PedidosApi';
import { CreateOrder } from '../api/PedidosApi';

const usePedidos = (userId) => {
	const [pedidos, setPedidos] = useState([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState(null);

	useEffect(() => {
		fetchPedidos();

		if (userId) {
			fetchPedidos();
		}
	}, [userId]);
	const fetchPedidos = async () => {
		try {
			const data = await getAllPedidosxUser(userId);
			console.log('pedidos', data);
			setPedidos(data.data);
		} catch (err) {
			setError(err);
		} finally {
			setLoading(false);
		}
	};
	const CreateOrderH = async (id) => {
		try {
			const data = await CreateOrder(id);
			fetchPedidos();
		} catch (err) {
			setError(err);
		} finally {
			setLoading(false);
		}
	};

	return { pedidos, loading, error,CreateOrderH };
};

export default usePedidos;
