# Управление миграцией

### Создание первой миграции
~~~
- cd ElectronicDiary
- dotnet ef migrations add InitialCreate
- dotnet ef database update
~~~

### Добавление миграции
~~~
 - cd ElectronicDiary
 - dotnet ef migrations add AddBlogCreatedTimestamp
 - dotnet ef database update
~~~

### Удаление миграции
~~~
- dotnet ef migrations remove
~~~