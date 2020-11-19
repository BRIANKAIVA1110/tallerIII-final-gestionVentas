-- phpMyAdmin SQL Dump
-- version 5.0.3
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 19-11-2020 a las 04:53:07
-- Versión del servidor: 10.4.14-MariaDB
-- Versión de PHP: 7.4.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `erp`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `articulos`
--

CREATE TABLE `articulos` (
  `Id` int(11) NOT NULL,
  `Descripcion` varchar(250) NOT NULL,
  `modeloId` int(11) NOT NULL,
  `colorId` int(11) NOT NULL,
  `MarcaId` int(11) NOT NULL,
  `CategoriaId` int(11) NOT NULL,
  `CodigoBarras` varchar(250) NOT NULL DEFAULT '',
  `precio` decimal(9,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `articulos`
--

INSERT INTO `articulos` (`Id`, `Descripcion`, `modeloId`, `colorId`, `MarcaId`, `CategoriaId`, `CodigoBarras`, `precio`) VALUES
(1, 'pepe', 2, 1, 6, 4, '0002000100060004', '191.00'),
(2, 'pepe2', 3, 3, 6, 4, '0003000200060004', '190.00'),
(3, 'Naranja', 2, 1, 1, 1, '0002000100010001', '1.00'),
(4, 'sadasdsad', 2, 6, 6, 4, '0002000500060004', '1.12'),
(6, 'daslkdasldk', 2, 11, 6, 4, '0002000900060004', '1.22');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categorias`
--

CREATE TABLE `categorias` (
  `Id` int(11) NOT NULL,
  `Codigo` varchar(4) NOT NULL,
  `Descripcion` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `categorias`
--

INSERT INTO `categorias` (`Id`, `Codigo`, `Descripcion`) VALUES
(1, '0001', 'Construccion'),
(2, '0002', 'Sanitarios'),
(3, '0003', 'Maquinaria'),
(4, '0004', 'Indumentaria');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `colores`
--

CREATE TABLE `colores` (
  `Id` int(11) NOT NULL,
  `Codigo` varchar(4) NOT NULL,
  `Descripcion` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `colores`
--

INSERT INTO `colores` (`Id`, `Codigo`, `Descripcion`) VALUES
(1, '0001', 'Rojo'),
(3, '0002', 'celeste'),
(4, '0003', 'Azul'),
(5, '0004', 'Blanco'),
(6, '0005', 'Verde'),
(7, '0006', 'Amarillo'),
(8, '0007', 'Gris'),
(9, '0008', 'Celeste'),
(11, '0009', 'Naranja');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleventas`
--

CREATE TABLE `detalleventas` (
  `Id` int(11) NOT NULL,
  `ArticuloId` int(11) DEFAULT NULL,
  `CantidadUnidades` decimal(9,2) DEFAULT NULL,
  `VentaId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `detalleventas`
--

INSERT INTO `detalleventas` (`Id`, `ArticuloId`, `CantidadUnidades`, `VentaId`) VALUES
(1, 3, '1.00', 1),
(2, 2, '3.00', 1),
(3, 1, '2.00', 2),
(4, 3, '3.00', 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `marcas`
--

CREATE TABLE `marcas` (
  `Id` int(11) NOT NULL,
  `Codigo` varchar(4) NOT NULL,
  `Descripcion` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `marcas`
--

INSERT INTO `marcas` (`Id`, `Codigo`, `Descripcion`) VALUES
(1, '0001', 'Black and Decker'),
(2, '0002', 'Bosch verde (gama bricolaje)'),
(3, '0003', 'Bosch azul (gama professional)'),
(4, '0004', 'DeWALT'),
(5, '0005', 'Einhell'),
(6, '0006', 'OMBU'),
(7, '0007', 'Hitachi');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `modelos`
--

CREATE TABLE `modelos` (
  `Id` int(11) NOT NULL,
  `Codigo` varchar(4) NOT NULL,
  `Descripcion` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `modelos`
--

INSERT INTO `modelos` (`Id`, `Codigo`, `Descripcion`) VALUES
(2, '0002', 'remera b'),
(3, '0003', 'remera c'),
(4, '0004', 'remera d'),
(5, '0005', 'remera e'),
(11, '0006', 'remera fffffffffff');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `stockarticulos`
--

CREATE TABLE `stockarticulos` (
  `Id` int(11) NOT NULL,
  `ArticuloId` int(11) NOT NULL,
  `Cantidad` decimal(9,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `stockarticulos`
--

INSERT INTO `stockarticulos` (`Id`, `ArticuloId`, `Cantidad`) VALUES
(1, 1, '2000.00'),
(3, 2, '40.00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventas`
--

CREATE TABLE `ventas` (
  `Id` int(11) NOT NULL,
  `TotalFinal` decimal(9,2) DEFAULT NULL,
  `FechaVenta` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `ventas`
--

INSERT INTO `ventas` (`Id`, `TotalFinal`, `FechaVenta`) VALUES
(1, '571.00', '2020-11-19'),
(2, '385.00', '2020-11-19');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `articulos`
--
ALTER TABLE `articulos`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `unique_codigobarra` (`CodigoBarras`),
  ADD KEY `fk_articulos_colores_colorId` (`modeloId`),
  ADD KEY `fk_articulos_modelos_modeloId` (`colorId`),
  ADD KEY `fk_articulos_marcas` (`MarcaId`),
  ADD KEY `fk_articulos_categorias` (`CategoriaId`);

--
-- Indices de la tabla `categorias`
--
ALTER TABLE `categorias`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `colores`
--
ALTER TABLE `colores`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `unique_colores_codigo` (`Codigo`);

--
-- Indices de la tabla `detalleventas`
--
ALTER TABLE `detalleventas`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_detallesVentas_ventas` (`VentaId`),
  ADD KEY `fk_detallesVentas_articulos` (`ArticuloId`);

--
-- Indices de la tabla `marcas`
--
ALTER TABLE `marcas`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `modelos`
--
ALTER TABLE `modelos`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `unique_modelos_codigo` (`Codigo`);

--
-- Indices de la tabla `stockarticulos`
--
ALTER TABLE `stockarticulos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_stockArticulo_articulos` (`ArticuloId`);

--
-- Indices de la tabla `ventas`
--
ALTER TABLE `ventas`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `articulos`
--
ALTER TABLE `articulos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `categorias`
--
ALTER TABLE `categorias`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `colores`
--
ALTER TABLE `colores`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `detalleventas`
--
ALTER TABLE `detalleventas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `marcas`
--
ALTER TABLE `marcas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `modelos`
--
ALTER TABLE `modelos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT de la tabla `stockarticulos`
--
ALTER TABLE `stockarticulos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `ventas`
--
ALTER TABLE `ventas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `articulos`
--
ALTER TABLE `articulos`
  ADD CONSTRAINT `fk_articulos_categorias` FOREIGN KEY (`CategoriaId`) REFERENCES `categorias` (`Id`),
  ADD CONSTRAINT `fk_articulos_colores_colorId` FOREIGN KEY (`modeloId`) REFERENCES `modelos` (`Id`),
  ADD CONSTRAINT `fk_articulos_marcas` FOREIGN KEY (`MarcaId`) REFERENCES `marcas` (`Id`),
  ADD CONSTRAINT `fk_articulos_modelos_modeloId` FOREIGN KEY (`colorId`) REFERENCES `colores` (`Id`);

--
-- Filtros para la tabla `detalleventas`
--
ALTER TABLE `detalleventas`
  ADD CONSTRAINT `fk_detallesVentas_articulos` FOREIGN KEY (`ArticuloId`) REFERENCES `articulos` (`Id`),
  ADD CONSTRAINT `fk_detallesVentas_ventas` FOREIGN KEY (`VentaId`) REFERENCES `ventas` (`Id`);

--
-- Filtros para la tabla `stockarticulos`
--
ALTER TABLE `stockarticulos`
  ADD CONSTRAINT `fk_stockArticulo_articulos` FOREIGN KEY (`ArticuloId`) REFERENCES `articulos` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
