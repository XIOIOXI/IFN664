public class MovieCollection
{
    private Movie[] movies = new Movie[100];
    private int count = 0;

    public bool Add(Movie movie)
    {
        if (count >= 100) return false;

        if (Get(movie.Title) != null)
        {
            return false; // Duplicate title
        }

        movies[count++] = movie;
        return true;
    }

    public bool Remove(string title)
    {
        for (int i = 0; i < count; i++)
        {
            if (movies[i] != null && movies[i].Title == title)
            {
                // Shift movies left
                for (int j = i; j < count - 1; j++)
                {
                    movies[j] = movies[j + 1];
                }
                movies[--count] = null;
                return true;
            }
        }
        return false;
    }

    public Movie Get(string title)
    {
        for (int i = 0; i < count; i++)
        {
            if (movies[i] != null && movies[i].Title == title)
            {
                return movies[i];
            }
        }
        return null;
    }

    public Movie[] GetAllMovies()
    {
        Movie[] result = new Movie[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = movies[i];
        }
        return result;
    }
}
