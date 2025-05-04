use hive_db;

CREATE TABLE account
(
  id BIGINT AUTO_INCREMENT PRIMARY KEY,
  user_id CHAR(64) NOT NULL UNIQUE,
  `password` CHAR(64) NOT NULL,  -- SHA-256 해시 결과는 항상 64 길이의 문자열
  create_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE login_token
(
    account_user_id VARCHAR(255) NOT NULL PRIMARY KEY,
    hive_token CHAR(64) NOT NULL,
    create_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    expires_time DATETIME NOT NULL
);
