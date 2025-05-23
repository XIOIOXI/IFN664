
using System;
public class TopThreeResult
{
    public int Count { get; set; }
    public Movie[] TopThree { get; set; }

    public TopThreeResult(int count, Movie[] topThree)
    {
        Count = count;
        TopThree = topThree;
    }
}

class Program
{
    static MovieCollection movieCollection = new MovieCollection();
    static MemberCollection memberCollection = new MemberCollection();

    static void Main(string[] args)
    {
        TestData.SeedTestData(movieCollection, memberCollection);
        
        // autometic test, when you test the functions, comment these two line
        RunEmpiricalTest();
        return;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=====================================================");
            Console.WriteLine("COMMUNITY LIBRARY MOVIE DVD MANAGEMENT SYSTEM");
            Console.WriteLine("=====================================================");
            Console.WriteLine("Main Menu");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Select from the following: ");
            Console.WriteLine("1. Staff");
            Console.WriteLine("2. Member");
            Console.WriteLine("0. End the program");
            Console.Write("Enter your choice ==> ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    StaffLogin();
                    break;
                case "2":
                    MemberLogin();
                    break;
                case "0":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    Pause();
                    break;
            }
        }
    }


    static void StaffLogin()
    {
        Console.Clear();
        Console.Write("Enter username: ");
        string username = Console.ReadLine();
        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        if (username == "staff" && password == "today123")
        {
            StaffMenu();
        }
        else
        {
            Console.WriteLine("Incorrect staff credentials.");
            Pause();
        }
    }

    static void StaffMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Staff Menu");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("1. Add DVDs to system");
            Console.WriteLine("2. Remove DVDs from system");
            Console.WriteLine("3. Register a new member to system");
            Console.WriteLine("4. Remove a registered member from system");
            Console.WriteLine("5. Find a member contact phone number, given the number's name");
            Console.WriteLine("6. Find members who are currently renting a particular movie");
            Console.WriteLine("0. Return to main menu");
            Console.Write("Enter your choice ==> ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddMovie();
                    break;
                case "2":
                    RemoveMovie();
                    break;
                case "3":
                    RegisterMember();
                    break;
                case "4":
                    DeleteMember();
                    break;
                case "5":
                    FindMemberPhone();
                    break;
                case "6":
                    FindMovieBorrowers();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    Pause();
                    break;
            }
        }
    }

    static void MemberLogin()
    {
        Console.Clear();
        Console.Write("Enter First Name: ");
        string first = Console.ReadLine();
        Console.Write("Enter Last Name: ");
        string last = Console.ReadLine();
        Console.Write("Enter 4-digit Password: ");
        string password = Console.ReadLine();

        var member = memberCollection.Find(first, last);
        if (member != null && member.Password == password)
        {
            MemberMenu(member);
        }
        else
        {
            Console.WriteLine("Invalid member credentials.");
            Pause();
        }
    }

    static void MemberMenu(Member member)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Member menu ({member.FullName})");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("1. Browse all the movies");
            Console.WriteLine("2. Display all the information about a movie, given the title of the movie");
            Console.WriteLine("3. Borrow a movie DVD");
            Console.WriteLine("4. Return a movie DVD");
            Console.WriteLine("5. List current borrowing movies");
            Console.WriteLine("6. Display the top 3 movies rented by the members");
            Console.WriteLine("0. Return to main menu");
            Console.Write("Enter your choice ==> ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllMovies();
                    break;
                case "2":
                    DisplayMovieDetails();
                    break;
                case "3":
                    BorrowDVD(member);
                    break;
                case "4":
                    ReturnDVD(member);
                    break;
                case "5":
                    ListBorrowedDVDs(member);
                    break;
                case "6":
                    ShowTop3MoviesWithCount();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    Pause();
                    break;
            }
        }
    }

    static void AddMovie()
    {
        Console.Clear();
        Console.Write("Enter movie title: ");
        string title = Console.ReadLine();

        var existing = movieCollection.Get(title);
        if (existing != null)
        {
            Console.Write("Movie exists. Enter number of additional copies to add: ");
            if (int.TryParse(Console.ReadLine(), out int extra))
            {
                existing.AddCopies(extra);
                Console.WriteLine("Copies added.");
            }
        }
        else
        {
            Console.Write("Enter genre: ");
            string genre = Console.ReadLine();
            Console.Write("Enter classification (G, PG, M15+, MA15+): ");
            string classification = Console.ReadLine();
            Console.Write("Enter duration (minutes): ");
            int duration = int.Parse(Console.ReadLine());
            Console.Write("Enter number of copies: ");
            int copies = int.Parse(Console.ReadLine());

            Movie newMovie = new Movie(title, genre, classification, duration, copies);
            if (movieCollection.Add(newMovie))
            {
                Console.WriteLine("New movie added.");
            }
            else
            {
                Console.WriteLine("Error: Could not add movie.");
            }
        }
        Pause();
    }

    static void RemoveMovie()
    {
        Console.Clear();
        Console.Write("Enter movie title: ");
        string title = Console.ReadLine();

        var movie = movieCollection.Get(title);
        if (movie == null)
        {
            Console.WriteLine("Movie not found.");
        }
        else
        {
            Console.Write("Enter number of copies to remove: ");
            int toRemove = int.Parse(Console.ReadLine());

            if (toRemove == movie.TotalCopies)
            {
                movieCollection.Remove(title);
                Console.WriteLine("All copies removed. Movie deleted.");
            }
            else if (movie.RemoveCopies(toRemove))
            {
                Console.WriteLine("Copies removed.");
            }
            else
            {
                Console.WriteLine("Error: Cannot remove more copies than available.");
            }
        }
        Pause();
    }

    static void RegisterMember()
    {
        Console.Clear();
        Console.Write("Enter first name: ");
        string first = Console.ReadLine();
        Console.Write("Enter last name: ");
        string last = Console.ReadLine();
        Console.Write("Enter contact phone: ");
        string phone = Console.ReadLine();
        Console.Write("Enter 4-digit password: ");
        string password = Console.ReadLine();

        if (memberCollection.Find(first, last) != null)
        {
            Console.WriteLine("Member already exists.");
        }
        else
        {
            Member newMember = new Member(first, last, phone, password);
            if (memberCollection.Add(newMember))
                Console.WriteLine("Member registered.");
            else
                Console.WriteLine("Registration failed.");
        }
        Pause();
    }

    static void DeleteMember()
    {
        Console.Clear();
        Console.Write("Enter first name: ");
        string first = Console.ReadLine();
        Console.Write("Enter last name: ");
        string last = Console.ReadLine();

        var member = memberCollection.Find(first, last);
        if (member == null)
        {
            Console.WriteLine("Member not found.");
        }
        else if (member.GetBorrowedTitles().Length > 0)
        {
            Console.WriteLine("Member has borrowed movies. Return all first.");
        }
        else
        {
            if (memberCollection.Remove(first, last))
                Console.WriteLine("Member removed.");
            else
                Console.WriteLine("Failed to remove member.");
        }
        Pause();
    }

    static void FindMemberPhone()
    {
        Console.Clear();
        Console.Write("Enter first name: ");
        string first = Console.ReadLine();
        Console.Write("Enter last name: ");
        string last = Console.ReadLine();

        var member = memberCollection.Find(first, last);
        if (member != null)
            Console.WriteLine($"Phone: {member.Phone}");
        else
            Console.WriteLine("Member not found.");
        Pause();
    }

    static void FindMovieBorrowers()
    {
        Console.Clear();
        Console.Write("Enter movie title: ");
        string title = Console.ReadLine();

        Member[] allMembers = memberCollection.GetAllMembers();
        Member[] borrowers = new Member[100];
        int count = 0;

        for (int i = 0; i < allMembers.Length; i++)
        {
            if (allMembers[i] == null) continue;
            string[] borrowed = allMembers[i].GetBorrowedTitles();
            for (int j = 0; j < borrowed.Length; j++)
            {
                if (borrowed[j] == null) break;
                if (borrowed[j] == title)
                {
                    borrowers[count++] = allMembers[i];
                    break;
                }
            }
        }

        if (count == 0)
        {
            Console.WriteLine("No one is currently borrowing this movie.");
        }
        else
        {
            Console.WriteLine("Borrowers:");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"- {borrowers[i].FullName} ({borrowers[i].Phone})");
            }
        }

        Pause();
    }


    static void DisplayAllMovies()
    {
        Console.Clear();
        Movie[] movies = movieCollection.GetAllMovies();
        Movie[] sorted = new Movie[movies.Length];
        Array.Copy(movies, sorted, movies.Length);

        // Manual bubble sort by title
        for (int i = 0; i < sorted.Length - 1; i++)
        {
            for (int j = i + 1; j < sorted.Length; j++)
            {
                if (sorted[i] != null && sorted[j] != null &&
                    string.Compare(sorted[i].Title, sorted[j].Title, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    var temp = sorted[i];
                    sorted[i] = sorted[j];
                    sorted[j] = temp;
                }
            }
        }

        Console.WriteLine("=== All Movies (Sorted by Title) ===");
        bool hasMovies = false;
        for (int i = 0; i < sorted.Length; i++)
        {
            if (sorted[i] != null)
            {
                Console.WriteLine(sorted[i].ToString());
                hasMovies = true;
            }
        }
        if (!hasMovies)
        {
            Console.WriteLine("No movies in collection.");
        }
        Pause();
    }

    static void DisplayMovieDetails()
    {
        Console.Clear();
        Console.Write("Enter movie title: ");
        string title = Console.ReadLine();
        Movie movie = movieCollection.Get(title);
        if (movie != null)
        {
            Console.WriteLine(movie.ToString());
        }
        else
        {
            Console.WriteLine("Movie not found.");
        }
        Pause();
    }

    static void BorrowDVD(Member member)
    {
        Console.Clear();
        Console.Write("Enter movie title: ");
        string title = Console.ReadLine();
        Movie movie = movieCollection.Get(title);

        if (movie == null)
        {
            Console.WriteLine("Movie not found.");
        }
        else if (!member.CanBorrow(title))
        {
            Console.WriteLine("Cannot borrow. Already borrowed or limit reached.");
        }
        else if (!movie.Borrow())
        {
            Console.WriteLine("No copies available.");
        }
        else
        {
            member.Borrow(title);
            Console.WriteLine("Borrowed successfully.");
        }
        Pause();
    }

    static void ReturnDVD(Member member)
    {
        Console.Clear();
        Console.Write("Enter movie title to return: ");
        string title = Console.ReadLine();
        Movie movie = movieCollection.Get(title);

        if (movie == null || !member.HasBorrowed(title))
        {
            Console.WriteLine("Invalid return. Not borrowed or doesn't exist.");
        }
        else
        {
            movie.Return();
            member.Return(title);
            Console.WriteLine("Returned successfully.");
        }
        Pause();
    }

    static void ListBorrowedDVDs(Member member)
    {
        Console.Clear();
        string[] borrowed = member.GetBorrowedTitles();
        bool hasAny = false;
        for (int i = 0; i < borrowed.Length; i++)
        {
            if (borrowed[i] != null)
            {
                if (!hasAny)
                {
                    Console.WriteLine("You are currently borrowing:");
                    hasAny = true;
                }
                Console.WriteLine("- " + borrowed[i]);
            }
        }
        if (!hasAny)
        {
            Console.WriteLine("You have not borrowed any DVDs.");
        }
        Pause();
    }

    public static TopThreeResult CountedFindTopThreeMovies(Movie[] movieArray)
    {
        int n = 0;
        Movie[] movieList = new Movie[1000];

        // Step 1: Extract non-null movies
        for (int i = 0; i < movieArray.Length; i++)
        {
            if (movieArray[i] != null)
            {
                movieList[n++] = movieArray[i];
            }
        }

        int count = 0;

        // Step 2: Selection Sort by BorrowCount (descending)
        for (int i = 0; i < n - 1; i++)
        {
            int max = i;
            for (int j = i + 1; j < n; j++)
            {
                count++;
                if (movieList[j].BorrowCount > movieList[max].BorrowCount)
                {
                    max = j;
                }
            }
            Movie temp = movieList[i];
            movieList[i] = movieList[max];
            movieList[max] = temp;
        }

        // Step 3: Extract top 3
        Movie[] topThree = new Movie[3];
        for (int i = 0; i < 3 && i < n; i++)
        {
            topThree[i] = movieList[i];
        }

        return new TopThreeResult(count, topThree);
    }

    static void ShowTop3MoviesWithCount()
    {
        Console.Clear();
        TopThreeResult result = CountedFindTopThreeMovies(movieCollection.GetTable());

        Console.WriteLine($"Total comparisons made: {result.Count}");
        Console.WriteLine("Top 3 Most Borrowed Movies:");

        for (int i = 0; i < result.TopThree.Length; i++)
        {
            Movie m = result.TopThree[i];
            if (m != null)
            {
                Console.WriteLine($"{i + 1}. {m.Title} - Borrowed {m.BorrowCount} times");
            }
        }

        Pause();
    }

    static void RunEmpiricalTest()
    {
        Console.Clear();
        Console.WriteLine("Running empirical test...");

        int[] sizes = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
        int trialsPerSize = 20;

        foreach (int n in sizes)
        {
            long totalCount = 0;

            for (int trial = 0; trial < trialsPerSize; trial++)
            {
                Movie[] table = GenerateSampleHashTable(n);
                TopThreeResult result = CountedFindTopThreeMovies(table);
                totalCount += result.Count;
            }

            double average = totalCount / (double)trialsPerSize;
            Console.WriteLine($"n = {n}, Average Comparisons = {average}");
        }

        Pause();
    }

    static Movie[] GenerateSampleHashTable(int n)
    {
        Movie[] table = new Movie[1000];
        Random rand = new Random();

        int inserted = 0;
        while (inserted < n)
        {
            int index = rand.Next(0, 1000);
            if (table[index] == null)
            {
                string title = "M_" + inserted;
                int borrow = rand.Next(0, 1000);
                Movie m = new Movie(title, "Genre", "PG", 120, 1);
                m.BorrowCount = borrow;
                table[index] = m;
                inserted++;
            }
        }

        return table;
    }

    static void Pause()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
