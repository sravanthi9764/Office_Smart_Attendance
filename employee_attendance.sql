SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

CREATE DATABASE IF NOT EXISTS employee_attendance DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE employee_attendance;

DROP TABLE IF EXISTS attendances;
CREATE TABLE IF NOT EXISTS attendances (
  username varchar(50) NOT NULL,
  attendances_datetime datetime DEFAULT current_timestamp(),
  attendances_date date DEFAULT curdate(),
  UNIQUE KEY username (username,attendances_date)
);


DROP TABLE IF EXISTS employees;
CREATE TABLE IF NOT EXISTS employees (
  username varchar(50) NOT NULL,
  fname varchar(50) NOT NULL,
  lname varchar(50) NOT NULL,
  phone varchar(50) NOT NULL,
  designation varchar(50) NOT NULL,
  email varchar(50) NOT NULL,
  password varchar(1000) NOT NULL,
  UNIQUE KEY username (username)
);


ALTER TABLE attendances
  ADD CONSTRAINT fk_username FOREIGN KEY (username) REFERENCES employees (username);
COMMIT;