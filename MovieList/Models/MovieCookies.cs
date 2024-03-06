using Microsoft.AspNetCore.Http;
using System.Data;
namespace MovieList.Models
{
    public class MovieCookies
    {
        private const string MyMovies = "mymovies";
        private const string Delimiter = "";
        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public MovieCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }
        public MovieCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }
        public void SetMyMovieIds(List<Movie> myMovies)
        {
            List<int> ids = myMovies.Select(t => t.MovieId).ToList();
            ids.ToString();
            string idsString = String.Join(Delimiter, ids);
            CookieOptions options = new CookieOptions { Expires = DateTime.Now.AddDays(30) };
            RemoveMyMovieIds();
            responseCookies.Append(MyMovies, idsString, options);
        }
        public string[] GetMyMovieIds()
        {
            string cookie = requestCookies[MyMovies];
            if (string.IsNullOrEmpty(cookie))
            {
                return new string[] { };
            }
            else
            {
                return cookie.Split(Delimiter);
            }
        }
        public void RemoveMyMovieIds()
        {
            responseCookies.Delete(MyMovies);
        }
    }
}
