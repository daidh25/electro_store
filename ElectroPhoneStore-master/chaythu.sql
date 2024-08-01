select * from AppUserLogins
select * from AppUserClaims
select * from AppRoles
select * from AppRoleClaims
select * from AppUsers
select * from AppUserLogins
select * from Products
select * from Reviews
select * from AppUserTokens
select * from Categories
select * from Coupons
select * from Languages
select * from OrderDetails
select * from Orders
drop database CNPM_NC_eShopSolution


select * from Categories
select * from Products
INSERT INTO Products (Name, CategoryId, Price, Stock, DateCreated, Description, Details, Thumbnail, ProductImage)
VALUES 
('Nokia 5.4', 6, 15000000, 5, '2024-03-31 20:28:57.4496804', 'Điện thoại Smartphone XYZ với các tính năng tiên tiến', 'Chi tiết sản phẩm điện thoại Smartphone XYZ', 'https://cdn.tgdd.vn/Products/Images/42/306994/Slider/vi-vn-samsung-galaxy-s23-fe-slider--(7).jpg', 'https://cdn.tgdd.vn/Products/Images/42/307174/samsung-galaxy-s24-ultra-xam-1.jpg');

DELETE FROM Products
WHERE Id = 8;