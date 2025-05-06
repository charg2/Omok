CREATE SCHEMA IF NOT EXISTS `game_db`;

use `game_db`;

CREATE TABLE IF NOT EXISTS player
(
  `user_id` BIGINT PRIMARY KEY,
  `nick_name` VARCHAR(64) NOT NULL,
  `create_time` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  `last_login_time` TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS mail
(
  `owner_id` BIGINT,
  `id` BIGINT AUTO_INCREMENT UNIQUE,
  `sender_id` BIGINT,
  `title` VARCHAR(64),
  `content` VARCHAR(256),
  `create_time` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`owner_id`, `id`)
);

CREATE TABLE IF NOT EXISTS friend
(
  `owner_id` BIGINT,
  `friend_id` BIGINT,
  `status` TINYINT,
  `add_time` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

  PRIMARY KEY ( `owner_id`, `friend_id` )
);

CREATE TABLE IF NOT EXISTS item
(
  `owner_id` BIGINT AUTO_INCREMENT PRIMARY KEY
);

