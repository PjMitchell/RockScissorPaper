
## Rock Scissor Paper

Rock Scissor Paper is a simple project to test and implement a number of techniques.

Rock beats Scissor, Scissor beats Paper and Paper beats rock.

The aim is to be able to collect statistics on choices in a rock paper scissor game.

Do people chose at random, and if not can bots be produced that take advantage of this fact.

Stage 1 is currently deployed on Azure [here](http://rockscissorpaper.azurewebsites.net/).

##Instructions

Nuget Package Restore is required to rebuild projects.

Additional ConnectionStrings.config file must be provided in RockScissorPaper root folder.

ConnectionStrings.config should provide connection string with name "DefaultConnection" that connections to a Mysql database 

