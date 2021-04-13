on 13 april by nilesh

in controller implemented method for 
1)global text class for global variable
2)method to export excel into db
3)insert subject by sunil
4)xra method to add foreign key subject id in qusn table
5)method to show subject


done following changes in sql
created table quesn

create table Question
(
QID int primary key identity,
Qsn varchar(200),
Opt1 varchar(200),
Opt2 varchar(200),
Opt3 varchar(200),
Opt4 varchar(200),
Answer varchar(200),
Level varchar(5),
FileName varchar(30),
SubjectId int FOREIGN KEY REFERENCES TestSubject(SubjectID)

)
