-- Connect as SYSDBA
-- Replace SYS_PASSWORD with your actual SYS password
-- CONNECT SYS/SYS_PASSWORD AS SYSDBA

-- Create the instrumentdb user if it doesn't exist
BEGIN
  -- First, attempt to drop the user if it exists (optional, comment out if not needed)
  BEGIN
    EXECUTE IMMEDIATE 'DROP USER instrumentdb CASCADE';
  EXCEPTION
    WHEN OTHERS THEN
      IF SQLCODE != -1918 THEN
        RAISE;
      END IF;
  END;
  
  -- Create the user
  EXECUTE IMMEDIATE 'CREATE USER instrumentdb IDENTIFIED BY InstrumentDb@123';
EXCEPTION
  WHEN OTHERS THEN
    IF SQLCODE != -1920 THEN
      RAISE;
    END IF;
END;
/

-- Grant necessary privileges
GRANT CREATE SESSION TO instrumentdb;
GRANT CREATE TABLE TO instrumentdb;
GRANT CREATE VIEW TO instrumentdb;
GRANT CREATE SEQUENCE TO instrumentdb;
GRANT CREATE PROCEDURE TO instrumentdb;
GRANT CREATE TRIGGER TO instrumentdb;

-- Grant unlimited tablespace (or specify a quota if needed)
GRANT UNLIMITED TABLESPACE TO instrumentdb;

-- Additional grants needed for Entity Framework Core
GRANT CREATE ANY TABLE TO instrumentdb;
GRANT ALTER ANY TABLE TO instrumentdb;
GRANT DROP ANY TABLE TO instrumentdb;
GRANT SELECT ANY TABLE TO instrumentdb;
GRANT INSERT ANY TABLE TO instrumentdb;
GRANT UPDATE ANY TABLE TO instrumentdb;
GRANT DELETE ANY TABLE TO instrumentdb;

-- If using identity columns (recommended)
GRANT SELECT ON SYS.ALL_TABLES TO instrumentdb;
GRANT SELECT ON SYS.ALL_TAB_COLUMNS TO instrumentdb; 