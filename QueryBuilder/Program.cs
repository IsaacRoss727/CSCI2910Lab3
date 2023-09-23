using Microsoft.Data.Sqlite;

namespace QueryBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
            var dbPath = root + "\\data\\data.db";
            List<Pokemon> pokemonList = new List<Pokemon>();
            List<BannedGame> bannedGameList = new List<BannedGame>();
            //Code to build the lists
            var pokemonDataPath = root + "\\Data\\AllPokemon.csv";
            using (StreamReader reader = new StreamReader(pokemonDataPath))
            {
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    string[] lineElements = line.Split(',');


                    //Constructs the Pokemon Object
                    Pokemon p = new Pokemon()
                    {
                        Id = int.Parse(lineElements[0]),
                        DexNumber = int.Parse(lineElements[0]),
                        Name = lineElements[1],
                        Form = lineElements[2],
                        Type1 = lineElements[3],
                        Type2 = lineElements[4],
                        Total = int.Parse(lineElements[5]),
                        HP = int.Parse(lineElements[6]),
                        Attack = int.Parse(lineElements[7]),
                        Defense = int.Parse(lineElements[8]),
                        SpecialDefense = int.Parse(lineElements[9]),
                        SpecialAttack = int.Parse(lineElements[10]),
                        Speed = int.Parse(lineElements[11]),
                        Generation = int.Parse(lineElements[12])
                    };

                    pokemonList.Add(p);
                }
            }

            //
            // I EDITED THE BANNED GAMES CSV TO ADD AN ID COLUMN!!! 
            // This code will not work on the one you provided. 
            //
            var bannedGamesDataPath = root + "\\Data\\BannedGames.csv";
            using (StreamReader reader = new StreamReader(bannedGamesDataPath))
            {
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    string[] lineElements = line.Split(',');


                    //Constructs the BannedGame Object
                    BannedGame b = new BannedGame()
                    {
                        Id = int.Parse(lineElements[0]),
                        Title = lineElements[1],
                        Series = lineElements[2],
                        Country = lineElements[3],
                        Details = lineElements[4]
                    };

                    bannedGameList.Add(b);
                }
            }

            using (var builder = new QueryBuilder(dbPath))
            {
               
                try
                {
                    builder.DeleteAll<Pokemon>();
                    Console.WriteLine("All Pokemon successfully deleted!");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"An error occured while deleting Pokemon: {ex.Message}");
                }


                try
                {
                    builder.DeleteAll<BannedGame>();
                    Console.WriteLine("All games successfully deleted!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occured while deleting games: {ex.Message}");
                }
                

                foreach (Pokemon mon in pokemonList)
                {
                    builder.Create(mon);
                    Console.WriteLine($"Pokemon {mon.Name} successfully added!");
                }

                foreach(BannedGame game in bannedGameList)
                {
                    builder.Create(game);
                    Console.WriteLine($"Game {game.Title} successfully added!");
                }

                try
                {
                    Pokemon testPokemon = new Pokemon(0, 1, "Name", "Form", "Type1", "Type2", 0, 0, 0, 0, 0, 0, 0, 1);
                    builder.Create(testPokemon);
                    Console.WriteLine($"Pokemon successfully added!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There was an error adding your Pokemon to the list: {ex.Message}");
                }

                try
                {
                    BannedGame testBannedGame = new BannedGame(137, "Title", "Series", "Country", "Details");
                    builder.Create(testBannedGame);
                    Console.WriteLine($"Game successfully added!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There was an error adding your game to the list: {ex.Message}");
                }
                


            }

        }
    }
}