USE master;
DROP DATABASE Clone_Quizizz_db;
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'Clone_Quizizz_db')
    CREATE DATABASE Clone_Quizizz_db;
Go
USE Clone_Quizizz_db;
CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(100) UNIQUE NOT NULL,
    hashed_password NVARCHAR(200) NOT NULL,
    email NVARCHAR(255) UNIQUE NOT NULL,
    phone NVARCHAR(20) NOT NULL,
    full_name NVARCHAR(255) NOT NULL,
    date_of_birth DATE,
    country NVARCHAR(200)
);

CREATE TABLE user_devices (
    id INT PRIMARY KEY IDENTITY, -- ID tự tăng, định danh duy nhất cho mỗi thiết bị của người dùng
    user_id INT NOT NULL, -- ID của người dùng liên kết với thiết bị
    device_id NVARCHAR(255) NOT NULL, -- ID của thiết bị (VD: UUID, IMEI, v.v.)
    FOREIGN KEY (user_id) REFERENCES users(id) -- liên kết khóa ngoại đến bảng users
);

CREATE TABLE flashcards (
    id INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính cho bảng Flashcards
    term NVARCHAR(255) NOT NULL, -- Từ vựng
    definition TEXT NOT NULL, -- Nghĩa của từ
    image NVARCHAR(MAX), -- URL hoặc đường dẫn đến hình ảnh
    created_date DATE, -- Ngày tạo
);

CREATE TABLE category (
    id INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính cho bảng Category
    categoryName NVARCHAR(255) NOT NULL -- Tên danh mục
);

CREATE TABLE user_category(
    user_id INT, --Khóa ngoại liên kết với bảng Users
    category_id INT, --Khóa ngoại liên kết với bảng Category
    FOREIGN KEY(user_id) REFERENCES users(id), -- liên kết khóa ngoại đến bảng Users
    FOREIGN KEY(category_id) REFERENCES category(id)-- liên kết khóa ngoại đến bảng Category
);

CREATE TABLE flashcard_category (
    flashcard_id INT, -- Khóa ngoại liên kết với bảng Flashcards
    category_id INT, -- Khóa ngoại liên kết với bảng Category
    FOREIGN KEY (flashcard_id) REFERENCES flashcards(id), -- liên kết khóa ngoại đến bảng Flashcard
    FOREIGN KEY (category_id) REFERENCES category(id), -- liên kết khóa ngoại đến bảng Category
);

CREATE TABLE user_portfolio (
    id INT PRIMARY KEY IDENTITY,
    user_id INT NOT NULL, -- Khóa ngoại liên kết với bảng Users
    description TEXT, -- Mô tả về portfolio
    quantity INT, -- số lượng flashcards
    created_date DATE, -- Ngày tạo
    -- Các cột khác liên quan đến thông tin cá nhân của người dùng (ví dụ: full_name, email, phone, v.v.)
);

CREATE TABLE notifications ( 
    id INT PRIMARY KEY IDENTITY(1,1), -- ID thông báo
    user_id INT NOT NULL,  -- ID người dùng 
    notification_type NVARCHAR(50), -- Loại thông báo (ví dụ: order_executed, price_alert, news_event)
    content TEXT NOT NULL, -- Nội dung thông báo 
    is_read BIT DEFAULT 0, -- Đánh dấu đã đọc hay chưa đọc (1: đã đọc, 0: chưa đọc)
    created_at DATETIME -- Thời điểm tạo thông báo
    FOREIGN KEY (user_id) REFERENCES users(id), -- Khóa ngoại liên kết với bảng Users
);


SELECT * FROM Clone_Quizizz_db.dbo.user_category WHERE user_id = 11 AND category_id = 3;
INSERT INTO user_category (user_id, category_id) VALUES (11, 3);
    SELECT * FROM users WHERE id = @@IDENTITY;
DELETE FROM flashcards WHERE ID = 90;

TRUNCATE TABLE flashcard

DROP DATABASE flashcard

--Select foreign key of users table
SELECT name, OBJECT_NAME(parent_object_id) as table_name FROM sys.objects WHERE object_id IN (
    SELECT fk.constraint_object_id FROM sys.foreign_key_columns AS fk
    WHERE fk.referenced_object_id = (SELECT object_id FROM sys.tables WHERE name = 'flashcard_category'))
GO


--Add constraint

-- ALTER TABLE flashcard_category
-- ADD CONSTRAINT FK_FlashCards_FlashcardCategory
-- FOREIGN KEY (flashcard_id)
-- REFERENCES flashcards(id);

-- ALTER TABLE flashcard_category
-- ADD CONSTRAINT FK_Category_FlashcardCategory
-- FOREIGN KEY (category_id)
-- REFERENCES category(id);

-- ALTER TABLE user_devices
-- ADD CONSTRAINT FK_UserDevices_Users
-- FOREIGN KEY (user_id)
-- REFERENCES users(id);

-- ALTER TABLE flashcards
-- ADD CONSTRAINT FK_FlashCards_Users
-- FOREIGN KEY (user_id)
-- REFERENCES users(id);

-- ALTER TABLE notifications
-- ADD CONSTRAINT FK_Notifications_Users
-- FOREIGN KEY (user_id)
-- REFERENCES users(id);

-- Delete Constraint

-- ALTER TABLE flashcard_category
-- DROP CONSTRAINT FK_FlashCards_FlashcardCategory;

-- ALTER TABLE flashcard_category
-- DROP CONSTRAINT FK_Category_FlashcardCategory;

-- ALTER TABLE user_devices
-- DROP CONSTRAINT FK__user_devi__user___3B75D760;

-- ALTER TABLE flashcards
-- DROP CONSTRAINT FK_FlashCards_Users;

-- ALTER TABLE notifications
-- DROP CONSTRAINT FK__notificat__creat__48CFD27E;

/*
-- Procedure Register user
 DROP PROCEDURE dbo.RegisterUser
 GO
CREATE PROCEDURE dbo.RegisterUser
    @username NVARCHAR(100),
    @password NVARCHAR(200),
    @email NVARCHAR(255),
    @phone NVARCHAR(20),
    @full_name NVARCHAR(255),
    @date_of_birth DATE,
    @country NVARCHAR(200)
AS
BEGIN
    INSERT INTO users (username, hashed_password, email, phone, full_name, date_of_birth, country)
    VALUES (@username, HASHBYTES('SHA2_256', @password), @email, @phone, @full_name, @date_of_birth, @country);
    SELECT * FROM users WHERE id = @@IDENTITY;
END

EXEC RegisterUser 'huy2', '123', 'huy2@gmail.com', '123', 'HuyHN', '1997-6-21', 'VN';

DELETE FROM users WHERE email='huy1@gmail.com'
*/

-- Procedure check login
CREATE PROCEDURE dbo.CheckLogin
    @Email NVARCHAR(50),
    @password NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @HashedPassword VARBINARY(32)
    SET @HashedPassword = HASHBYTES('SHA2_256', @password);
    BEGIN
        SELECT * FROM users WHERE Email IN
        (SELECT email FROM users WHERE email = @Email AND hashed_password = @HashedPassword);
    END
END

EXEC CheckLogin 'huy1@gmail.com', '123';