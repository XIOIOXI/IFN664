
using System;

class Program
{
    static MovieCollection movieCollection = new MovieCollection();
    static MemberCollection memberCollection = new MemberCollection();

    static void Main(string[] args)
    {
        SeedTestData();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Community Library System ===");
            Console.WriteLine("1. Staff Login");
            Console.WriteLine("2. Member Login");
            Console.WriteLine("0. Exit");
            Console.Write("Select option: ");
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

    static void SeedTestData()
    {
        movieCollection.Add(new Movie("Inception", "Sci-Fi", "PG", 148, 5));
        movieCollection.Add(new Movie("Coco", "Family", "G", 105, 3));
        movieCollection.Add(new Movie("Titanic", "Drama", "PG", 195, 2));
        movieCollection.Add(new Movie("Avengers", "Action", "M15+", 140, 4));
        movieCollection.Add(new Movie("The Matrix", "Sci-Fi", "M15+", 136, 6));

        Member m1 = new Member("Alice", "Wang", "0400111222", "1234");
        Member m2 = new Member("Bob", "Lee", "0411222333", "5678");

        m1.Borrow("Inception");
        movieCollection.Get("Inception").Borrow();

        memberCollection.Add(m1);
        memberCollection.Add(m2);
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
            Console.WriteLine("=== Staff Menu ===");
            Console.WriteLine("1. Add Movie DVDs");
            Console.WriteLine("2. Remove Movie DVDs");
            Console.WriteLine("3. Register a New Member");
            Console.WriteLine("4. Delete a Member");
            Console.WriteLine("5. Display Member Contact Phone");
            Console.WriteLine("6. Display Members Renting a Movie");
            Console.WriteLine("0. Return to Main Menu");
            Console.Write("Select option: ");
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
            Console.WriteLine($"=== Member Menu ({member.FullName}) ===");
            Console.WriteLine("1. Display All Movie DVDs");
            Console.WriteLine("2. Display Movie Details");
            Console.WriteLine("3. Borrow Movie DVD");
            Console.WriteLine("4. Return Movie DVD");
            Console.WriteLine("5. List My Borrowed DVDs");
            Console.WriteLine("6. Show Top 3 Most Borrowed Movies");
            Console.WriteLine("0. Return to Main Menu");
            Console.Write("Select option: ");
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
                    ShowTop3Movies();
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

    static void ShowTop3Movies()
    {
        Console.Clear();
        Movie[] movies = movieCollection.GetAllMovies();
        Movie[] top = new Movie[movies.Length];
        Array.Copy(movies, top, movies.Length);

        // Sort by BorrowCount descending
        for (int i = 0; i < top.Length - 1; i++)
        {
            for (int j = i + 1; j < top.Length; j++)
            {
                if (top[i] != null && top[j] != null && top[j].BorrowCount > top[i].BorrowCount)
                {
                    var temp = top[i];
                    top[i] = top[j];
                    top[j] = temp;
                }
            }
        }

        Console.WriteLine("Top 3 Most Borrowed Movies:");
        int shown = 0;
        for (int i = 0; i < top.Length && shown < 3; i++)
        {
            if (top[i] != null && top[i].BorrowCount > 0)
            {
                Console.WriteLine($"{top[i].Title} - Borrowed {top[i].BorrowCount} times");
                shown++;
            }
        }

        if (shown == 0)
        {
            Console.WriteLine("No movies have been borrowed yet.");
        }

        Pause();
    }


    static void Pause()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
