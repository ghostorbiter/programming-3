using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
	// https://grouplens.org/datasets/movielens/1m/
	public class Program
	{
		public static void Main(string[] args)
		{
			// You are provided with a database with three tables: database.Movies, database.Users, database.Ratings
			// You can obtain information about available columns (fields) by looking at classes Movie, User, RatingEntry
			// In the Users table a field Age is rather age group (range) identifier than actual age. E.g. 1 means a range 1-18.
			// Every movie has its unique id in MovieID, every user has its unique id in UserID.
			// Both these tables (Movies, Users), are connected by Ratings: a single rating is provided by a certain user for a certain movie.
			DatabaseMovies database = DatabaseMovies.GetInstance("movies.csv", "users.csv", "ratings.csv");

			// stage 1

			// Count a fraction of males and females in a Users table
			// Order (ascending) by a gender
			var fract = from user in database.Users
						group user by user.Gender into tmp
						orderby tmp.Key ascending
						select new { tmp.Key, fractee = (double)tmp.Count() / database.Users.Count() };

			foreach (var f in fract)
                Console.WriteLine($"{f.Key,-15} {f.fractee} ");
			// your solution

			Console.WriteLine($"--------------");

			// stage 2

			// Count a fraction of males and females are in every age group
			// Sort (ascending) a result by age and nextly by a gender.

			var fract1 = from user in database.Users
						group user by new { user.Gender, user.Age } into tmp
						orderby tmp.Key.Age ascending
						select new { tmp.Key, fractee = (double)tmp.Count() / database.Users.Count() };


			foreach (var f in fract1)
				Console.WriteLine($"{f.Key.Age, -5} {f.Key.Gender} - {f.fractee} ");

			// your solution

			Console.WriteLine($"--------------");

			// stage 3

			// Filter out (into a result) movies whose one of genres starts with a letter "M".
			// The result should skip first 3 movies and contain next 6 movies.

			var movies = from movie in database.Movies
						 let x = movie.Genres.Split('|')
						 from str in x
						 where str.StartsWith('M')
						 select movie;

			movies = movies.Skip(3).Take(6);

			foreach ( var movie in movies)
                Console.WriteLine($"{movie.MovieID, - 5} {movie.Title, -15} {movie.Genres}");

			 // your solution

			 Console.WriteLine($"--------------");

			// stage 4

			// We define remakes of a single movie as a set of movies which title is the same (but years of production are different)
			// Your result should contain all movies, which are remakes (print all movies, for which we can find at least one different movie with the same title).
			// Print all details of movies, but also additionally: minimum year of remake set, maximum year, and a difference between minimum and maximum.			

			var movies1 = from movie in database.Movies
						  group movie by movie.Title into tmp
						  where tmp.Count() > 1
						  select new { tmp.Key, mini = tmp.Min(mov => mov.Year), maxi = tmp.Max(mov => mov.Year) };

			foreach (var m in movies1)
            {
                Console.WriteLine($"{m.Key} {m.mini} {m.maxi} {m.maxi - m.mini} ");
			}

			// your solution

			Console.WriteLine($"--------------");

			// stage 5

			// Sort (descending) movies by average rating, but with distinction for votes of males and females.
			// Result should skip first 40 movies and contain the next 20 movies.	

			var rates = from rate in database.Ratings
						let x = rate.MovieID
						let y = rate.UserID
						join user in database.Users on y equals user.UserID
						join movie in database.Movies on x equals movie.MovieID
						group rate by new { movie.Title, user.Gender } into tmp
						select new { tmp.Key.Title, tmp.Key.Gender, Rating = tmp.Average(r => r.Rating) } into pmt
						orderby pmt.Rating descending
						select new { pmt.Title, pmt.Gender, pmt.Rating };


			rates = rates.Skip(40).Take(20);
			foreach(var m in rates)
                Console.WriteLine($"{m.Title, -25} {m.Gender} {m.Rating}");

			// your solution

			Console.WriteLine($"--------------");
		}
	}
}