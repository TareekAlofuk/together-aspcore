CREATE TABLE member { 
    Id INTEGER SERIAL NOT NULL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Phone VARCHAR(50),
    Phone2 VARCHAR(50),
    Email VARCHAR (100),
    Address Text ,
    BirthDate DATE , 
    JobTitle VARCHAR (100),
    Disabled BIT,
    Archived BIT ,
    Passport VARCHAR (255),
    FaceImage VARCHAR (255)
}