CREATE TABLE auth.tbl_users (
	user_id serial4 NOT NULL,
	user_name varchar(100) NOT NULL,
	email_address varchar(250) NOT NULL,
	"password" varchar(100) NOT NULL,
	created_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	updated_at timestamp NULL,
	CONSTRAINT tbl_users_email_address_key UNIQUE (email_address),
	CONSTRAINT tbl_users_pkey PRIMARY KEY (user_id)
);
