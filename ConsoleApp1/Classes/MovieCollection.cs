public class MovieCollection
{
    private Movie[] table = new Movie[1000];
    
    private int Hash (string title)
    {
        int hash = 0;
        for (int i = 0; i < title.Length; i++)
        {
            hash = (hash * 31 + title[i]) % 1000;
        }
        return hash;
    }

    public bool Add(Movie movie)
    {
        int index = Hash (movie.Title);

        for (int i = 0; i < table.Length; i++)
        {
            int probeIndex = (index + i) % table.Length;

            if (table[probeIndex] == null)
            {
                table[probeIndex] = movie;
                return true;
            }
            else if (table[probeIndex].Title == movie.Title)
            {
                return false;
            }
        }

        return false; //table is full
    }

    public bool Remove(string title)
    {
        int index = Hash(title);

        for (int i = 0; i < table.Length; i++)
        {
            int probeIndex = (index + i) % table.Length;

            if (table[probeIndex] == null)
                return false;

            if (table[probeIndex].Title == title)
            {
                table[probeIndex] = null; // Lazy deletion
                return true;
            }
        }

        return false;
    }

    public Movie Get(string title)
    {
        int index = Hash(title);

        for (int i = 0; i < table.Length; i++)
        {
            int probeIndex = (index + i) % table.Length;

            if (table[probeIndex] == null)
                return null;

            if (table[probeIndex].Title == title)
                return table[probeIndex];
        }

        return null;
    }

    public Movie[] GetAllMovies()
    {
        int count = 0;
        for (int i = 0; i < table.Length; i++)
        {
            if (table[i] != null) count++;
        }

        Movie[] result = new Movie[count];
        int idx = 0;

        for (int i = 0; i < table.Length; i++)
        {
            if (table[i] != null)
            {
                result[idx++] = table[i];
            }
        }

        return result;
    }


    // Algorithm GetTopThreeMovies
    public Movie[] GetTopThreeMovies()
    {
        Movie[] all = GetAllMovies();
        int n = all.Length;

        // Bubble sort by BorrowCount descending
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (all[j].BorrowCount < all[j + 1].BorrowCount)
                {
                    Movie temp = all[j];
                    all[j] = all[j + 1];
                    all[j + 1] = temp;
                }
            }
        }

        Movie[] top3 = new Movie[3];
        for (int i = 0; i < 3 && i < n; i++)
        {
            top3[i] = all[i];
        }

        return top3;
    }
}
