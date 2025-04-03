create table ARTIST(
ID_ NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
NAME_ VARCHAR2(100) NOT NULL,
COUNTRY VARCHAR2(100) NOT NULL,
DESCRIPCION VARCHAR2(250)
);

alter table ARTIST
 add constraint PK_ID_
 primary key(ID_);
 
 
create table PAINT(
ID_ NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
NAME_  VARCHAR2(100) NOT NULL,
TYPE_ VARCHAR2(100) NOT NULL,
DESCRIPCION VARCHAR2(250),
ID_ARTIST NUMBER,
DELETE_ CHAR(1) DEFAULT(0)
);

 alter table PAINT
  add constraint FK_ID_
  foreign key (ID_ARTIST)
  references ARTIST (ID_);
  
--sp ACTUALIZAR 

 create or replace procedure SP_UPDATE_ARTIST(ID_IN in number, NAME_IN in varchar2,COUNTRY_IN in varchar2,DESCRIPCION_IN in varchar2)
 as
 begin
  update ARTIST set Name_=NAME_IN,
  COUNTRY=COUNTRY_IN,
  DESCRIPCION=DESCRIPCION_IN
  where ID_=ID_IN;
 end;

--sp delete paint


 create or replace procedure SP_DELETE_PAINT(ID_IN in number)
 as
 begin
  update PAINT set DELETE_=1
  where ID_=ID_IN;
 end;

execute SP_UPDATE_ARTIST( '1','Jorge Luis','El Salvador','salvadoreño se inicio en pintura a los 10 años')
execute SP_DELETE_PAINT( '1')


insert into ARTIST(NAME_, COUNTRY, DESCRIPCION) values('Jose Luis','Guatemala','Artista guatemalteco, se inicio en la pintura desde los 8 años');
insert into PAINT(NAME_,TYPE_,DESCRIPCION,ID_ARTIST) values('El ojo','Tecnica en oleo','Inspirado en la belleza de cada mirada',1);
insert into PAINT(NAME_,TYPE_,DESCRIPCION,ID_ARTIST) values('La selva','Tecnica en oleo','Inspirado en la belleza de la naturaleza',1);


select * from ARTIST;
select * from PAINT;

UPDATE PAINT SET DELETE_=0
  