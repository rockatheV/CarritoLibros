/** @format */

import { useRouter } from 'next/router';
import React, { useEffect } from 'react';
import usePedidos from '../hooks/HookPedidos';
import { Table, Button } from 'flowbite-react';
import '../app/globals.css';
import PedidosSVG from '../IconosSVG/PediosSVG';

const PedidosList = () => {
	const router = useRouter();
	const { userId } = router.query;

	const { pedidos, loading, error, CreateOrderH } = usePedidos(userId);
	const handleDetPedidosSelect = (orderId) => {
		router.push(`/DetPedidos/${orderId}`);
	};
	const CreateOrder = async () => {
		const fetchedBook = await CreateOrderH(userId);
	};
	if (loading) return <p>Loading...</p>;
	if (error) return <p>Error loading pedidos.</p>;

	return (
		<div>
			<div className='flex justify-between items-center mb-5'>
				<div>
					<h1 className='text-5xl font-extrabold dark:text-white'>
						<small className='ms-2 font-semibold text-gray-500 dark:text-gray-400'>
							Lista de Ordenes
						</small>
					</h1>
				</div>
				<div>
					<Button color='success' onClick={CreateOrder}>
						Crear Orden
					</Button>
				</div>
			</div>
			<Table>
				<Table.Head>
					<Table.HeadCell>FechaPedido</Table.HeadCell>
					<Table.HeadCell>CantidadDetallesPedido</Table.HeadCell>
					<Table.HeadCell>TotalPedido</Table.HeadCell>

					<Table.HeadCell>Acciones</Table.HeadCell>
					<Table.HeadCell>
						<span className='sr-only'>Pedidos</span>
					</Table.HeadCell>
				</Table.Head>
				<Table.Body className='divide-y'>
					{pedidos.map((order) => (
						<Table.Row
							key={order.id}
							className='bg-white dark:border-gray-700 dark:bg-gray-800'
						>
							<Table.Cell className='whitespace-nowrap font-medium text-gray-900 dark:text-white'>
								{new Date(order.fechaPedido).toLocaleString('es-ES', {
									year: 'numeric',
									month: '2-digit',
									day: '2-digit',
									hour: '2-digit',
									minute: '2-digit',
								})}
							</Table.Cell>
							<Table.Cell>{order.quantity}</Table.Cell>
							<Table.Cell>{order.priceTotal}</Table.Cell>

							<Table.Cell>
								<a
									onClick={() => handleDetPedidosSelect(order.id)}
									className='font-medium text-cyan-600 hover:underline dark:text-cyan-500'
								>
									<PedidosSVG width={20} height={20} stroke='black' />
								</a>
							</Table.Cell>
						</Table.Row>
					))}
				</Table.Body>
			</Table>
		</div>
	);
};

export default PedidosList;
