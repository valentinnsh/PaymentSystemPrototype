
create table if not exists users
(
 id            int not null,
 first_name    varchar(50) not null,
 last_name     varchar(50) not null,
 email         varchar(50) not null,
 registered_at timestamp not null,
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
