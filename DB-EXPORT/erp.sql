-- phpMyAdmin SQL Dump
-- version 5.0.3
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 26-11-2020 a las 09:10:42
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
(6, 'daslkdasldk', 11, 11, 6, 4, '0006000900060004', '1.21'),
(10, 'prueba stock', 2, 1, 1, 11, '0002000155111100', '100.00');

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
(4, '0004', 'Indumentaria'),
(11, '1100', 'pruba');

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
(6, '0005', 'Verde'),
(7, '0006', 'Amarillo'),
(11, '0009', 'Naranja'),
(20, '4001', 'prueba1'),
(21, '4002', 'PRUEBA2'),
(22, '4003', 'prueba3'),
(24, '3333', 'asdasd');

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
(4, 3, '3.00', 2),
(5, 1, '2.00', 3),
(6, 3, '2.00', 3),
(7, 3, '10.00', 4),
(8, 2, '2.00', 4),
(9, 1, '1.00', 4),
(10, 1, '10.00', 6),
(11, 2, '2.00', 6),
(12, 6, '1.00', 8),
(13, 2, '40.00', 8),
(14, 6, '1.00', 9),
(15, 6, '1.00', 10),
(16, 2, '1.00', 10),
(17, 2, '1.00', 11),
(18, 1, '1.00', 11),
(19, 1, '1.00', 12),
(20, 1, '1.00', 13),
(21, 1, '1.00', 14),
(22, 1, '1.00', 15),
(23, 1, '1.00', 16),
(24, 6, '1.00', 17),
(25, 1, '1.00', 18),
(26, 1, '1.00', 19),
(27, 6, '1.00', 20),
(28, 1, '1.00', 21),
(29, 6, '1.00', 22),
(30, 1, '1.00', 23),
(31, 6, '1.00', 24),
(32, 1, '1.00', 25),
(33, 1, '1.00', 26),
(34, 1, '1.00', 27),
(35, 1, '1.00', 28),
(36, 1, '1.00', 29),
(37, 2, '1.00', 29),
(38, 1, '1.00', 30),
(39, 2, '1.00', 30),
(40, 2, '1.00', 31),
(41, 6, '1.00', 31),
(42, 6, '1.00', 32),
(43, 6, '1.00', 33),
(44, 2, '1.00', 33),
(45, 1, '3.00', 33),
(46, 1, '1.00', 35),
(47, 6, '1.00', 35),
(48, 1, '1.00', 36),
(49, 6, '1.00', 36),
(50, 1, '1.00', 37),
(51, 1, '1.00', 38),
(52, 6, '1.00', 38),
(53, 1, '1.00', 39),
(54, 6, '1.00', 39),
(55, 1, '1.00', 40),
(56, 1, '1.00', 41),
(57, 6, '1.00', 41),
(58, 2, '1.00', 41),
(59, 1, '1.00', 42),
(60, 6, '1.00', 42),
(61, 2, '3.00', 42),
(62, 1, '1.00', 43),
(63, 3, '1.00', 44),
(64, 1, '1.00', 45),
(65, 3, '1.00', 45),
(66, 3, '1.00', 46),
(67, 2, '1.00', 46),
(68, 1, '1.00', 47),
(69, 2, '3.00', 47),
(70, 1, '1.00', 48),
(71, 1, '1.00', 49),
(72, 3, '1.00', 49),
(73, 2, '1.00', 49);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `empresa`
--

CREATE TABLE `empresa` (
  `Id` int(11) NOT NULL,
  `RazonSocial` varchar(250) NOT NULL,
  `Telefono` varchar(12) NOT NULL,
  `Cuit` varchar(12) NOT NULL,
  `Domicilio` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `empresa`
--

INSERT INTO `empresa` (`Id`, `RazonSocial`, `Telefono`, `Cuit`, `Domicilio`) VALUES
(1, 'PEPE S.A', '1122334411', '27-1111111-5', 'Av. pepe 1234, Lomas de Zamora, Buenos Aires');

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
(1, '5511', 'Black and Decker'),
(2, '0002', 'Bosch verde (gama bricolaje)'),
(3, '0003', 'Bosch azul (gama professional)'),
(4, '0004', 'DeWALT'),
(5, '0005', 'Einhell'),
(6, '0006', 'OMBU'),
(7, '0007', 'Hitachi'),
(8, '4411', 'sadasds'),
(9, '3311', 'sadasds'),
(10, '2211', 'sadasds'),
(11, '1100', 'asdasd');

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
(11, '0006', 'remera fffffffffff'),
(20, '0001', 'prueba');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `modulosapplicacion`
--

CREATE TABLE `modulosapplicacion` (
  `Id` int(11) NOT NULL,
  `Descripcion` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `modulosapplicacion`
--

INSERT INTO `modulosapplicacion` (`Id`, `Descripcion`) VALUES
(1, 'articulos'),
(2, 'stock'),
(3, 'ventas'),
(4, 'reportes'),
(5, 'seguridad');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `modulosxperfiles`
--

CREATE TABLE `modulosxperfiles` (
  `Id` int(11) NOT NULL,
  `PerfilId` int(11) NOT NULL,
  `ModuloId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `modulosxperfiles`
--

INSERT INTO `modulosxperfiles` (`Id`, `PerfilId`, `ModuloId`) VALUES
(29, 1, 1),
(30, 1, 2),
(31, 1, 3),
(32, 1, 4),
(33, 1, 5),
(39, 21, 1),
(40, 21, 4);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `perfiles`
--

CREATE TABLE `perfiles` (
  `Id` int(11) NOT NULL,
  `Descripcion` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `perfiles`
--

INSERT INTO `perfiles` (`Id`, `Descripcion`) VALUES
(1, 'admin'),
(21, 'pepe');

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
(1, 1, '1968.00'),
(3, 2, '85.00'),
(4, 4, '150.00'),
(5, 6, '90.00'),
(11, 3, '12.00'),
(13, 10, '1.00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `Id` int(11) NOT NULL,
  `UserName` varchar(256) NOT NULL,
  `Password` varchar(256) NOT NULL,
  `PerfilId` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`Id`, `UserName`, `Password`, `PerfilId`) VALUES
(1, 'admin', '\nYWRtaW4=', 1),
(4, 'brianvtb', 'MTIzNA==', 21);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventas`
--

CREATE TABLE `ventas` (
  `Id` int(11) NOT NULL,
  `TotalFinal` decimal(9,2) DEFAULT NULL,
  `FechaVenta` datetime NOT NULL,
  `ClienteInformacion` varchar(250) DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `ventas`
--

INSERT INTO `ventas` (`Id`, `TotalFinal`, `FechaVenta`, `ClienteInformacion`) VALUES
(1, '571.00', '2020-11-19 00:00:00', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(2, '385.00', '2020-11-19 00:00:00', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(3, '384.00', '2020-11-20 00:00:00', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(4, '581.00', '2020-11-20 00:00:00', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(6, '2290.00', '2020-11-20 14:40:48', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(8, '7601.22', '2020-11-21 22:10:00', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(9, '1.22', '2020-11-22 00:36:39', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(10, '191.22', '2020-11-22 00:57:44', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(11, '381.00', '2020-11-22 01:22:33', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(12, '191.00', '2020-11-22 01:25:28', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(13, '191.00', '2020-11-22 01:31:30', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(14, '191.00', '2020-11-22 01:32:24', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(15, '191.00', '2020-11-22 01:37:35', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(16, '191.00', '2020-11-22 01:41:48', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(17, '1.22', '2020-11-22 01:43:03', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(18, '191.00', '2020-11-22 01:53:57', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(19, '191.00', '2020-11-22 01:56:34', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(20, '1.22', '2020-11-22 01:57:15', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(21, '191.00', '2020-11-22 02:00:57', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(22, '1.22', '2020-11-22 02:15:44', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(23, '191.00', '2020-11-22 02:17:06', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(24, '1.22', '2020-11-22 02:25:26', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(25, '191.00', '2020-11-22 02:26:28', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(26, '191.00', '2020-11-22 02:27:49', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(27, '191.00', '2020-11-22 02:28:44', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(28, '191.00', '2020-11-22 02:31:10', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(29, '381.00', '2020-11-22 20:32:39', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(30, '381.00', '2020-11-22 20:37:35', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(31, '191.22', '2020-11-22 20:41:53', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(32, '1.22', '2020-11-22 22:26:22', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(33, '764.22', '2020-11-22 22:27:41', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(35, '192.22', '2020-11-22 23:51:48', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(36, '192.22', '2020-11-22 23:54:24', 'Villarroel torrico brian;av. falsa 123;lomas de zamora;0000;1111111111'),
(37, '191.00', '2020-11-23 00:55:11', 'brian;falsa123;ldz;1832;11111'),
(38, '192.22', '2020-11-23 01:54:47', 'brian;falsa123;ldz;1832;111111'),
(39, '192.22', '2020-11-23 01:56:34', 'brian;falsa 123;ldz;1832;11111111'),
(40, '191.00', '2020-11-23 01:58:11', 'asdsad;asd;asd;-;asd'),
(41, '382.22', '2020-11-24 01:42:51', 'sadasdasd;sadsda;asdasd;asdasd;asd'),
(42, '762.22', '2020-11-24 16:31:41', 'pepe;1234 false;ldz;1832;123123123'),
(43, '191.00', '2020-11-24 21:07:21', 'asdsad;asdas;asd;asd;asdasd'),
(44, '1.00', '2020-11-24 23:55:16', 'brian;falsa123;ldz;1832;123213'),
(45, '192.00', '2020-11-24 23:55:59', 'brian;asd;asd;asd;asd'),
(46, '191.00', '2020-11-25 00:13:47', 'brian;pepe123;ldz;1832;1562512282'),
(47, '761.00', '2020-11-25 00:14:27', 'brian;falsa123;asdasd;1832;1111111'),
(48, '191.00', '2020-11-25 00:17:13', 'brian;1234falsa;ldz;1832;1562512282'),
(49, '382.00', '2020-11-26 05:06:19', 'brian;monte 584;lomas de zamora;1832;1562512282');

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
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `unique_cateogoria_codigo` (`Codigo`);

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
-- Indices de la tabla `empresa`
--
ALTER TABLE `empresa`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `marcas`
--
ALTER TABLE `marcas`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `unique_codigo_marca` (`Codigo`);

--
-- Indices de la tabla `modelos`
--
ALTER TABLE `modelos`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `unique_modelos_codigo` (`Codigo`);

--
-- Indices de la tabla `modulosapplicacion`
--
ALTER TABLE `modulosapplicacion`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `modulosxperfiles`
--
ALTER TABLE `modulosxperfiles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `unique_perfilid_moduloId` (`PerfilId`,`ModuloId`);

--
-- Indices de la tabla `perfiles`
--
ALTER TABLE `perfiles`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `stockarticulos`
--
ALTER TABLE `stockarticulos`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `unique_articuloId` (`ArticuloId`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `unique_useName` (`UserName`),
  ADD KEY `fk_usuario_perfiles` (`PerfilId`);

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
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `categorias`
--
ALTER TABLE `categorias`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `colores`
--
ALTER TABLE `colores`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT de la tabla `detalleventas`
--
ALTER TABLE `detalleventas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=74;

--
-- AUTO_INCREMENT de la tabla `empresa`
--
ALTER TABLE `empresa`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `marcas`
--
ALTER TABLE `marcas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `modelos`
--
ALTER TABLE `modelos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT de la tabla `modulosapplicacion`
--
ALTER TABLE `modulosapplicacion`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `modulosxperfiles`
--
ALTER TABLE `modulosxperfiles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=41;

--
-- AUTO_INCREMENT de la tabla `perfiles`
--
ALTER TABLE `perfiles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT de la tabla `stockarticulos`
--
ALTER TABLE `stockarticulos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `ventas`
--
ALTER TABLE `ventas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=50;

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

--
-- Filtros para la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD CONSTRAINT `fk_usuario_perfiles` FOREIGN KEY (`PerfilId`) REFERENCES `perfiles` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
