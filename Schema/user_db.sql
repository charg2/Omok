CREATE SCHEMA IF NOT EXISTS `user_db`;

use `user_db`;

CREATE TABLE IF NOT EXISTS user
(
  `id` BIGINT AUTO_INCREMENT PRIMARY KEY,
  `account` CHAR(64) NOT NULL UNIQUE,
  `password` CHAR(64) NOT NULL,  -- SHA-256 해시 결과는 항상 64 길이의 문자열
  `create_time` TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS auth_token
(
    `account` VARCHAR(255) NOT NULL PRIMARY KEY,
    `hive_token` CHAR(64) NOT NULL,
	`user_id` BIGINT,
    `create_time` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `expire_time` DATETIME NOT NULL
);
