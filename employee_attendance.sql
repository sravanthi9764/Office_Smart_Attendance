CREATE DATABASE IF NOT EXISTS employee_attendance;
USE employee_attendance;

DROP TABLE IF EXISTS attendances;
CREATE TABLE IF NOT EXISTS attendances (
  username varchar(50) NOT NULL,
  attendances_datetime datetime DEFAULT current_timestamp(),
  attendances_date date DEFAULT curdate(),
  Remark varchar(100) DEFAULT NULL,
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
  Role varchar(50) DEFAULT NULL,
  UNIQUE KEY username (username)
);

ALTER TABLE attendances
  ADD CONSTRAINT fk_username FOREIGN KEY (username) REFERENCES employees (username);
COMMIT;