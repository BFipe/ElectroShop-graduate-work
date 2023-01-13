-- Замените myId на ваш айдишник с таблицы
SELECT * from AspNetUsers


INSERT INTO AspNetRoles
VALUES
(
 'f8330221-2620-4f8f-a1da-d777ad5fb9cf', 'Admin', 'ADMIN', 'erca5454-6bdd-497b-bde5-8f7b7e92c473'
)

GO

-- Замените myId здесь
INSERT INTO TechnoShopUserRoles
VALUES
('myId','f8330221-2620-4f8f-a1da-d777ad5fb9cf')

GO

-- Замените myId здесь
UPDATE AspNetUsers
SET
    EmailConfirmed = 1
WHERE Id = 'myId'


