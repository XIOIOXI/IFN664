public class Member
{
    public string FirstName;
    public string LastName;
    public string Phone;
    public string Password;

    private string[] borrowed = new string[10];
    private int borrowCount = 0;

    public Member(string first, string last, string phone, string pwd)
    {
        FirstName = first;
        LastName = last;
        Phone = phone;
        Password = pwd;
    }

    public string FullName => FirstName + " " + LastName;

    public bool CanBorrow(string title)
    {
        if (borrowCount >= 10) return false;
        for (int i = 0; i < borrowCount; i++)
        {
            if (borrowed[i] == title) return false;
        }
        return true;
    }

    public void Borrow(string title)
    {
        if (borrowCount < 10)
        {
            borrowed[borrowCount++] = title;
        }
    }

    public void Return(string title)
    {
        for (int i = 0; i < borrowCount; i++)
        {
            if (borrowed[i] == title)
            {
                for (int j = i; j < borrowCount - 1; j++)
                {
                    borrowed[j] = borrowed[j + 1];
                }
                borrowed[--borrowCount] = null;
                break;
            }
        }
    }

    public string[] GetBorrowedTitles()
    {
        string[] result = new string[borrowCount];
        for (int i = 0; i < borrowCount; i++)
        {
            result[i] = borrowed[i];
        }
        return result;
    }

    public bool HasBorrowed(string title)
    {
        for (int i = 0; i < borrowCount; i++)
        {
            if (borrowed[i] == title) return true;
        }
        return false;
    }
}
