/** @format */

// src/components/Layout.js
import React from 'react';
import Head from 'next/head';

const Layout = ({ children }) => {
	return (
		<>
			<Head>
				<title>Tu título aquí</title>
				<meta name='description' content='Descripción de tu página aquí' />
				<link rel='icon' href='/favicon.ico' />
			</Head>
			<div className='min-h-screen flex flex-col'>
				{/* Encabezado opcional */}
				<header className='bg-gray-800 text-white py-4 px-8'>
					<div className='max-w-11xl mx-auto flex justify-between items-center'>
						<div className='text-xl font-bold'>BookShop</div>
						<nav className='space-x-4'>
							<a href='/UsersPage' className='text-gray-300 hover:text-white'>
								Usuarios
							</a>
							<a href='/BooksPage' className='text-gray-300 hover:text-white'>
								Libros
							</a>
						</nav>
					</div>
				</header>
				{/* Contenido principal */}
				<main className='flex-1'>
					<div className='max-w-10xl mx-auto px-4 sm:px-6 lg:px-8 py-8'>
						{children}
					</div>
				</main>
				{/* Pie de página opcional */}
				<footer className='bg-gray-800 text-white py-4'>
					<div className='max-w-7xl mx-auto text-center'>
						&copy; {new Date().getFullYear()} BookShop. Todos los derechos
						reservados.
					</div>
				</footer>
			</div>
		</>
	);
};

export default Layout;
