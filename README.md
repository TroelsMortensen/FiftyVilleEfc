# FiftyVilleEfc

The purpose of this project is to practice LINQ.

You are given some setup code, with a class called Queries. It's a unit test class, so the idea is you can write methods here for different queries.

The case is theft mystery, and you must use the included database to extract data and solve the mystery of who stole the rubber duck from fifty ville.

Use the AppContext (see examples in the Queries class) to extract data and deduce who the thief is, and who their accomplish is.

I have included a function which can take a list of objects, and print them out in a nice table format. This code is in the ListToTable class. I don't expect you to inspect it.

Most of the entities have navigation properties between them. A few entities are without relationships. Below is a class diagram of the entities.
* A primary key is marked {PK}
* A composite primary key is marked {PPK}
* A foreign key is marked {FK}
* A navigation property is marked {NP}

<img src="https://github.com/TroelsMortensen/FiftyVilleEfc/blob/master/FiftyVilleEfc/FiftyVilleEfc.png" alt="drawing" width="400"/>

## Setup
First, you need to go to the AppContext class, and change the data source string, so that it points to your local version of the database.db file. You will notice the path is currently to a place on my local pc.\
You must change the path to the absolute path of your database.db. In Rider, you can right click the file --> Copy path/reference.

## Description

A rubber duck was stolen from Fiftyville.

The town of Fiftyville wants you to solve the mystery of the stolen duck.
Authorities believe that the theif/thieves stole the duck and then, shortly afterwards, took a flight out of town, with the help of an accomplice. 
Your goal is to identify:

1) Who the thief is,
2) What city the thief escaped to,
3) Who the thief's accomplice is who helped them escape

All you know is that 
- The theft took place on July 28.
- Took place on Chamberlin Street

Come up with a list of suspects. Can you narrow it down to 8? How much further down can you narrow your suspects?

## Credits
*This exercise was borrowed from https://cs50.harvard.edu/x/2021/seminars/
It was originally an SQL adventure, using SQLite, and I converted it for C# and EFC, and updated the dataset to follow the typical constraints like referential integrity.
