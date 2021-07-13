
create database ode;

create table ode.account(
ONR int not null AUTO_INCREMENT,
mail varchar(60),
pin int,
name varchar(64),
primary key (ONR)
);


create table ode.dept(
DNR int PRIMARY KEY not null AUTO_INCREMENT,
ammount double,
label varchar(255),
currency varchar(4),
ONR int not null,
deptOwnerONR int not null ,
foreign key(ONR) references account(ONR),
foreign key(deptOwnerONR) references account(ONR)
);




create table ode.contact(
LNR int PRIMARY KEY not null AUTO_INCREMENT,
name varchar(255),
ONR int not null,
contactONR int not null ,
foreign key(ONR) references account(ONR),
foreign key(contactONR) references account(ONR)
);


