/** @format */

// src/components/BooksList.js
import React, { useState } from 'react';
import useUsers from '../hooks/UsersHooks';
import { useRouter } from 'next/router';

import { Table } from 'flowbite-react';
import '../app/globals.css';
import PedidosSVG from '../IconosSVG/PediosSVG';

const UsersList = () => {
	const { users, loading, error } = useUsers();
	const router = useRouter();

	const handleUserSelect = (userId) => {
		router.push(`/Pedidos/${userId}`);
	};

	if (loading) return <p>Loading...</p>;
	if (error) return <p>Error loading Books.</p>;

	return (
		<div>
			<div>
				<div className='mb-5'>
					<h1 class='text-5xl font-extrabold dark:text-white'>
						<small class='ms-2 font-semibold text-gray-500 dark:text-gray-400'>
							Lista de Usuarios
						</small>
					</h1>
				</div>
				<Table>
					<Table.Head>
						<Table.HeadCell>Nombre</Table.HeadCell>
						<Table.HeadCell>Email</Table.HeadCell>

						<Table.HeadCell>Acciones</Table.HeadCell>
						<Table.HeadCell>
							<span className='sr-only'>Pedidos</span>
						</Table.HeadCell>
					</Table.Head>
					<Table.Body className='divide-y'>
						{users.map((user) => (
							<Table.Row
								key={user.id}
								className='bg-white dark:border-gray-700 dark:bg-gray-800'
							>
								<Table.Cell className='whitespace-nowrap font-medium text-gray-900 dark:text-white'>
									{user.name}
								</Table.Cell>
								<Table.Cell>{user.email}</Table.Cell>

								<Table.Cell>
									<a
										onClick={() => handleUserSelect(user.id)}
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
		</div>
	);
};

export default UsersList;
