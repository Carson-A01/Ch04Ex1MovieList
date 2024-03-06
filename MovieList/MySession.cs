using Microsoft.AspNetCore.Http;
using MovieList.Models;
namespace MovieList
{
    public class MySession
    {
        private const string MovieKey = "movies";
        private ISession session { get; set; }
        public MySession(ISession sess)
        {
            session = sess;
        }
        public List<Movie> getMovies => session.GetObject<List<Movie>>(MovieKey) ?? new List<Movie>();
        public void SetMovies(List<Movie> movies) => session.SetObject(MovieKey, movies);
    }

    public class MovieSession
    {
        private const string MovieKey = "mymovies";
        private const string CountKey = "moviecount";
        private const string ConfKey = "confkey";
        private const string DivKey = "divkey";

        private ISession session { get; set; }
        public MovieSession(ISession session)
        {
            this.session = session;
        }
        public void SetMyMovies(List<Movie> movies)
        {
            session.SetObject(MovieKey, movies);
            session.SetInt32(CountKey, movies.Count);
        }
        public List<Movie> GetMyMovies => session.GetObject<List<Movie>>(MovieKey) ?? new List<Movie>();
        public int GetMyMovieCount() => session.GetInt32(CountKey) ?? 0;
        public void SetActiveConf(string activeConf) => session.SetString(ConfKey, activeConf);
        public string GetActiveConf() => session.GetString(ConfKey);
        public void SetActiveDiv(string activeDiv) => session.SetString(DivKey, activeDiv);
        public void GetActiveDiv() => session.GetString(DivKey);
        public void RemoveMyMovies()
        {
            session.Remove(MovieKey);
            session.Remove(CountKey);
        }
    }
}
