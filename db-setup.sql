-- CREATE TABLE burgers (
--   id int NOT NULL AUTO_INCREMENT,
--   name VARCHAR(255) NOT NULL,
--   description VARCHAR(255) NOT NULL,
--   price DECIMAL() NOT NULL,
--   PRIMARY KEY(id)
-- );

-- INSERT INTO burgers (name, description, price)
-- VALUES ("The Plain Jane", "Burger on a bun", 7.99);

-- SELECT * FROM burgers;

-- ALTER TABLE burgers MODIFY COLUMN price DECIMAL(10, 2); 

-- updates the table 'burgers' to have price 7.99 where the id is => 1
-- UPDATE burgers SET (price = 7.99) WHERE id = 1; 

-- UPDATE burgers SET
--   price = 7.99,
--   name = 'The Plain Jane with Cheese',
--   description = "Burger on a bun with cheese"
--   WHERE id = 1;

-- DELETE FROM burgers WHERE id = 1;

CREATE TABLE smoothies (
  id int NOT NULL AUTO_INCREMENT,
  name VARCHAR(200) NOT NULL,
  description VARCHAR(200) NOT NULL,
  price DECIMAL(10, 2) NOT NULL,
  PRIMARY KEY(id)
);