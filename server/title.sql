DROP DATABASE IF EXISTS `scrollTitle`;
CREATE DATABASE `scrollTitle`;
USE `scrollTitle`;
CREATE TABLE title(
    `id` INT PRIMARY KEY AUTO_INCREMENT,
    `content` TEXT,
    `status` INT(1)
);
