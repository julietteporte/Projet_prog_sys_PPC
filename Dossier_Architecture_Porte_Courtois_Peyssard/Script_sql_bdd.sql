#------------------------------------------------------------
#        Script MySQL.
#------------------------------------------------------------


#------------------------------------------------------------
# Table: Recipe
#------------------------------------------------------------

CREATE TABLE Recipe(
        Id                  int (11) Auto_increment  NOT NULL ,
        Name                Varchar (255) ,
        Number_people_serve Int ,
        Preparation_time    Int ,
        Cooking_time        Int ,
        Rest_time           Int ,
        Instruction         Varchar (255) ,
        Price               Float ,
        Is_menu             Bool ,
        Is_daily_specials   Bool ,
        Is_available        Bool ,
        Id_Recipe_category  Int ,
        PRIMARY KEY (Id )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Server
#------------------------------------------------------------

CREATE TABLE Server(
        Id       int (11) Auto_increment  NOT NULL ,
        Dish_Max Int ,
        PRIMARY KEY (Id )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Bar
#------------------------------------------------------------

CREATE TABLE Bar(
        Capacity_dish_max       Int NOT NULL ,
        Capacity_dirty_dish     Int ,
        Capacity_dish           Int ,
        Capacity_dirty_dish_max Int ,
        Ready_to_wash           Bool ,
        PRIMARY KEY (Capacity_dish_max )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Table
#------------------------------------------------------------

CREATE TABLE Table(
        Id        int (11) Auto_increment  NOT NULL ,
        Capacity  Int ,
        Quantity  Int ,
        Id_Square Int ,
        PRIMARY KEY (Id )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Square
#------------------------------------------------------------

CREATE TABLE Square(
        Id int (11) Auto_increment  NOT NULL ,
        PRIMARY KEY (Id )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Ingredients
#------------------------------------------------------------

CREATE TABLE Ingredients(
        Id                      int (11) Auto_increment  NOT NULL ,
        Name                    Varchar (255) ,
        Quantity                Int ,
        Arrival_date            Date ,
        Id_Ingredients_category Int ,
        PRIMARY KEY (Id )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Ingredients_category
#------------------------------------------------------------

CREATE TABLE Ingredients_category(
        Id   int (11) Auto_increment  NOT NULL ,
        Name Varchar (255) ,
        PRIMARY KEY (Id )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Recipe_line
#------------------------------------------------------------

CREATE TABLE Recipe_line(
        Id               int (11) Auto_increment  NOT NULL ,
        Ingrediens_order Int ,
        Quantity         Int ,
        Id_Ingredients   Int ,
        Id_Recipe        Int ,
        PRIMARY KEY (Id )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Recipe_type
#------------------------------------------------------------

CREATE TABLE Recipe_type(
        Id   int (11) Auto_increment  NOT NULL ,
        Name Varchar (255) ,
        PRIMARY KEY (Id )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Recipe_category
#------------------------------------------------------------

CREATE TABLE Recipe_category(
        Id   int (11) Auto_increment  NOT NULL ,
        Name Varchar (255) ,
        PRIMARY KEY (Id )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Materials
#------------------------------------------------------------

CREATE TABLE Materials(
        Id                   int (11) Auto_increment  NOT NULL ,
        Name                 Varchar (255) ,
        Quantity             Int ,
        Id_Material_category Int ,
        PRIMARY KEY (Id )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Material_category
#------------------------------------------------------------

CREATE TABLE Material_category(
        Id   int (11) Auto_increment  NOT NULL ,
        Name Varchar (255) ,
        PRIMARY KEY (Id )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: have
#------------------------------------------------------------

CREATE TABLE have(
        Id        Int NOT NULL ,
        Id_Recipe Int NOT NULL ,
        PRIMARY KEY (Id ,Id_Recipe )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: use
#------------------------------------------------------------

CREATE TABLE use(
        Id        Int NOT NULL ,
        Id_Recipe Int NOT NULL ,
        PRIMARY KEY (Id ,Id_Recipe )
)ENGINE=InnoDB;

ALTER TABLE Recipe ADD CONSTRAINT FK_Recipe_Id_Recipe_category FOREIGN KEY (Id_Recipe_category) REFERENCES Recipe_category(Id);
ALTER TABLE Table ADD CONSTRAINT FK_Table_Id_Square FOREIGN KEY (Id_Square) REFERENCES Square(Id);
ALTER TABLE Ingredients ADD CONSTRAINT FK_Ingredients_Id_Ingredients_category FOREIGN KEY (Id_Ingredients_category) REFERENCES Ingredients_category(Id);
ALTER TABLE Recipe_line ADD CONSTRAINT FK_Recipe_line_Id_Ingredients FOREIGN KEY (Id_Ingredients) REFERENCES Ingredients(Id);
ALTER TABLE Recipe_line ADD CONSTRAINT FK_Recipe_line_Id_Recipe FOREIGN KEY (Id_Recipe) REFERENCES Recipe(Id);
ALTER TABLE Materials ADD CONSTRAINT FK_Materials_Id_Material_category FOREIGN KEY (Id_Material_category) REFERENCES Material_category(Id);
ALTER TABLE have ADD CONSTRAINT FK_have_Id FOREIGN KEY (Id) REFERENCES Recipe_type(Id);
ALTER TABLE have ADD CONSTRAINT FK_have_Id_Recipe FOREIGN KEY (Id_Recipe) REFERENCES Recipe(Id);
ALTER TABLE use ADD CONSTRAINT FK_use_Id FOREIGN KEY (Id) REFERENCES Materials(Id);
ALTER TABLE use ADD CONSTRAINT FK_use_Id_Recipe FOREIGN KEY (Id_Recipe) REFERENCES Recipe(Id);
