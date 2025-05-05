public class Movie
{
    public string Title;
    public string Genre;
    public string Classification;
    public int Duration;
    public int TotalCopies;
    public int AvailableCopies;
    public int BorrowCount;

    public Movie(string title, string genre, string classification, int duration, int copies)
    {
        Title = title;
        Genre = genre;
        Classification = classification;
        Duration = duration;
        TotalCopies = copies;
        AvailableCopies = copies;
        BorrowCount = 0;
    }

    public void AddCopies(int number)
    {
        TotalCopies += number;
        AvailableCopies += number;
    }

    public bool RemoveCopies(int number)
    {
        if (number > AvailableCopies) return false;
        AvailableCopies -= number;
        TotalCopies -= number;
        return true;
    }

    public bool Borrow()
    {
        if (AvailableCopies > 0)
        {
            AvailableCopies--;
            BorrowCount++;
            return true;
        }
        return false;
    }

    public void Return()
    {
        if (AvailableCopies < TotalCopies)
        {
            AvailableCopies++;
        }
    }

    public override string ToString()
    {
        return $"{Title} | Genre: {Genre}, Class: {Classification}, Duration: {Duration} mins, Available: {AvailableCopies}/{TotalCopies}";
    }
}
