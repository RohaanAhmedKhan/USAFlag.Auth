CREATE OR REPLACE PROCEDURE auth.sp_create_user(IN emailaddress text, IN password text, IN username text, INOUT result integer)
 LANGUAGE plpgsql
AS $procedure$
BEGIN

 IF EXISTS (SELECT 1 FROM auth.TBL_USERS WHERE email_address = emailAddress) THEN
        result := 1; -- User already exists
ELSE
    -- Insert a new user, automatically generating UserId and CreatedAt
    INSERT INTO auth.tbl_users (email_address, password, user_name)
    VALUES (emailAddress, password, userName);

    result := 0; -- Success
END IF;

  
END;
$procedure$
;