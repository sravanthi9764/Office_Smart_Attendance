-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 10, 2022 at 03:27 PM
-- Server version: 10.4.25-MariaDB
-- PHP Version: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `employee_attendance`
--
CREATE DATABASE IF NOT EXISTS `employee_attendance` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `employee_attendance`;

-- --------------------------------------------------------

--
-- Table structure for table `attendances`
--

DROP TABLE IF EXISTS `attendances`;
CREATE TABLE IF NOT EXISTS `attendances` (
  `username` varchar(50) NOT NULL,
  `attendances_datetime` datetime DEFAULT current_timestamp(),
  `attendances_date` date DEFAULT curdate(),
  UNIQUE KEY `username` (`username`,`attendances_date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `attendances`
--

INSERT INTO `attendances` (`username`, `attendances_datetime`, `attendances_date`) VALUES
('Steve', '2022-11-10 19:52:34', '2022-11-10');

-- --------------------------------------------------------

--
-- Table structure for table `employees`
--

DROP TABLE IF EXISTS `employees`;
CREATE TABLE IF NOT EXISTS `employees` (
  `username` varchar(50) NOT NULL,
  `fname` varchar(50) NOT NULL,
  `lname` varchar(50) NOT NULL,
  `phone` varchar(50) NOT NULL,
  `designation` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `password` varchar(1000) NOT NULL,
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `employees`
--

INSERT INTO `employees` (`username`, `fname`, `lname`, `phone`, `designation`, `email`, `password`) VALUES
('Steve', 'Steve', 'Austin', '9856321458', 'Software Eng.', 'steve.a@gmail.com', '123456');

--
-- Constraints for dumped tables
--

--
-- Constraints for table `attendances`
--
ALTER TABLE `attendances`
  ADD CONSTRAINT `fk_username` FOREIGN KEY (`username`) REFERENCES `employees` (`username`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
