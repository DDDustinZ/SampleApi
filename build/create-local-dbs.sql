IF DB_ID('Sales') IS NOT NULL
    BEGIN
        ALTER DATABASE [Sales] set single_user with rollback immediate
        DROP DATABASE [Sales];
    END;
GO
CREATE DATABASE [Sales];
GO
