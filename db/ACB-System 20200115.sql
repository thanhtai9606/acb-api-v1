-- phpMyAdmin SQL Dump
-- version 5.0.0
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 172.17.0.3
-- Thời gian đã tạo: Th1 15, 2020 lúc 03:57 PM
-- Phiên bản máy phục vụ: 8.0.18
-- Phiên bản PHP: 7.4.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `ACB-System`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `customer`
--

CREATE TABLE `customer` (
  `customer_id` int(11) NOT NULL,
  `customer_name` text COLLATE utf8mb4_vietnamese_ci NOT NULL,
  `phone` varchar(20) COLLATE utf8mb4_vietnamese_ci NOT NULL,
  `address` text COLLATE utf8mb4_vietnamese_ci NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `customer`
--

INSERT INTO `customer` (`customer_id`, `customer_name`, `phone`, `address`, `modified_date`) VALUES
(3, 'Dũng Lê', '1234', 'Tan Uyenzz', '2019-12-29 16:00:29'),
(12, 'abc', '12343322', 'Tan Uyenzss', '2020-01-05 02:57:53'),
(17, 'Tài', '0986080835', '3104 Doctors Drive', '2020-01-15 15:44:04');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `product`
--

CREATE TABLE `product` (
  `product_id` int(11) NOT NULL,
  `product_name` text COLLATE utf8mb4_vietnamese_ci NOT NULL,
  `order_price` int(11) NOT NULL DEFAULT '0',
  `sale_price` int(11) NOT NULL DEFAULT '0',
  `model` text COLLATE utf8mb4_vietnamese_ci NOT NULL,
  `inventory` smallint(6) NOT NULL,
  `warranty` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `product`
--

INSERT INTO `product` (`product_id`, `product_name`, `order_price`, `sale_price`, `model`, `inventory`, `warranty`, `modified_date`) VALUES
(2, 'Camera 360', 22000, 23000, 'xx', 20, 36, '2020-01-05 10:14:00'),
(4, 'Man cua', 50000, 51000, 'xx', 4, 3, '2020-01-06 12:51:42'),
(5, 'Máy tính Toshiba', 14000, 20000, 'Toshiba', 2, 2, '2020-01-12 15:49:33');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `sale_detail`
--

CREATE TABLE `sale_detail` (
  `id` int(11) NOT NULL,
  `so_id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `price` int(11) NOT NULL DEFAULT '0',
  `total_amount` int(11) DEFAULT NULL,
  `warranty_start` date NOT NULL,
  `warranty_end` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `sale_detail`
--

INSERT INTO `sale_detail` (`id`, `so_id`, `product_id`, `quantity`, `price`, `total_amount`, `warranty_start`, `warranty_end`) VALUES
(1, 24, 4, 3, 2000, 6000, '2020-01-12', '2020-04-12'),
(2, 24, 4, 3, 1000, 3000, '2020-01-12', '2020-04-12'),
(3, 25, 5, 1, 1000, 1000, '2020-01-12', '2020-03-12'),
(4, 26, 2, 1, 1000, 1000, '2020-01-13', '2023-01-13'),
(5, 26, 2, 1, 1000, 1000, '2020-01-13', '2023-01-13'),
(6, 27, 2, 1, 50000, 50000, '2020-01-13', '2023-01-13'),
(7, 27, 4, 1, 1000, 1000, '2020-01-13', '2020-04-13'),
(8, 28, 2, 2, 70000, 140000, '2020-01-13', '2023-01-13'),
(9, 28, 5, 1, 1000, 1000, '2020-01-13', '2020-03-13'),
(10, 29, 4, 3, 51000, 153000, '2020-01-15', '2020-04-15'),
(11, 30, 2, 1, 23000, 23000, '2020-01-15', '2023-01-15'),
(12, 31, 5, 1, 20000, 20000, '2020-01-15', '2020-03-15'),
(13, 31, 4, 3, 51000, 153000, '2020-01-15', '2020-04-15');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `sale_header`
--

CREATE TABLE `sale_header` (
  `so_id` int(11) NOT NULL,
  `customer_id` int(11) NOT NULL,
  `total_line` int(11) NOT NULL,
  `sub_total` int(11) NOT NULL DEFAULT '0',
  `discount` double NOT NULL DEFAULT '0',
  `tax` double NOT NULL DEFAULT '0',
  `create_by` varchar(20) COLLATE utf8mb4_vietnamese_ci NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `sale_header`
--

INSERT INTO `sale_header` (`so_id`, `customer_id`, `total_line`, `sub_total`, `discount`, `tax`, `create_by`, `modified_date`) VALUES
(24, 3, 9000, 0, 0, 0, '', '2020-01-12 16:25:53'),
(25, 3, 1000, 0, 0, 0, '', '2020-01-12 16:26:03'),
(26, 3, 2000, 0, 0, 0, '', '2020-01-13 10:47:41'),
(27, 3, 51000, 0, 5, 10, '', '2020-01-13 12:45:20'),
(28, 3, 155100, 0, 0, 10, '', '2020-01-13 12:47:41'),
(29, 12, 153000, 0, 0, 0, '', '2020-01-15 15:03:44'),
(30, 0, 24840, 23000, 2, 10, '', '2020-01-15 15:40:54'),
(31, 17, 181650, 173000, 2, 7, '', '2020-01-15 15:48:51');

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`customer_id`);

--
-- Chỉ mục cho bảng `product`
--
ALTER TABLE `product`
  ADD PRIMARY KEY (`product_id`);

--
-- Chỉ mục cho bảng `sale_detail`
--
ALTER TABLE `sale_detail`
  ADD PRIMARY KEY (`id`),
  ADD KEY `sale_detail_ibfk_1` (`so_id`);

--
-- Chỉ mục cho bảng `sale_header`
--
ALTER TABLE `sale_header`
  ADD PRIMARY KEY (`so_id`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `customer`
--
ALTER TABLE `customer`
  MODIFY `customer_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT cho bảng `product`
--
ALTER TABLE `product`
  MODIFY `product_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT cho bảng `sale_detail`
--
ALTER TABLE `sale_detail`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT cho bảng `sale_header`
--
ALTER TABLE `sale_header`
  MODIFY `so_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `sale_detail`
--
ALTER TABLE `sale_detail`
  ADD CONSTRAINT `sale_detail_ibfk_1` FOREIGN KEY (`so_id`) REFERENCES `sale_header` (`so_id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

