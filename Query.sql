


create database watermarking;
create table tbl_userdetails(usercode varchar(10)primary key,
                             username varchar(35)unique,
                             password varchar(15),
                             usertype varchar(10));
                             
create table tbl_bankdetails(bankcode varchar(10)primary key,
                             bankid varchar(15)unique,
                             bankname varchar(100),
                             acctype varchar(15),
                             address varchar(50),
                             address2 varchar(50),
                             area varchar(50),
                             currency varchar(35),
                             maxamount numeric(10,2),
                             phoneno varchar(15),
                             pincode int,
                             state varchar(15),
                             banktype varchar(15),
                             banklogo image);                             
                             
create table tbl_accountdetails(acccode varchar(10)primary key,
                                accholdername varchar(35),
                                accid varchar(15)unique,
                                fathername varchar(35),
                                mothername varchar(35),
                                age int,
                                state varchar(35),
                                city varchar(35),
                                area varchar(35),
                                pincode int,
                                phoneno varchar(15),
                                status varchar(1),
                                signature image,
                                iniamount numeric(10,2),
                                acctype varchar(10),
                                usercode varchar(10)references tbl_userdetails(usercode),
                                bankcode varchar(10)references tbl_bankdetails(bankcode));
                                
create table tbl_transactionDetails(transid varchar(10)primary key,
                                    fromacc varchar(10),
                                    toacc varchar(10),
                                    ondate datetime,
                                    amount numeric(10,2));                                 
                                
                                                             