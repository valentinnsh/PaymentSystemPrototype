
create table if not exists users
(
 id            serial,
 first_name    varchar(50) not null,
 last_name     varchar(50) not null,
 email         varchar(50) not null,
 password      varchar(50) not null,
 registered_at timestamptz not null,
 primary key ( id )
);

create table if not exists balances
(
 user_id int not null,
 amount  bigint not null,
 primary key ( user_id ),
 foreign key ( user_id ) references users ( id )
);

create index if not exists user_email_index on users
(
 email
);

insert into users(first_name,last_name,email, password, registered_at)
values('Igor', 'Igorev', 'Igor@gmail.com', 'password', to_timestamp('2017-03-31 9:30:20','YYYY-MM-DD HH:MI:SS')at time zone 'Etc/UTC');
 