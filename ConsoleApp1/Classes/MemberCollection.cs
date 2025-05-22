public class MemberCollection
{
    private Member[] members = new Member[100];
    private int count = 0;

    public bool Add(Member m)
    {
        if (count >= 100) return false;
        if (Find(m.FirstName, m.LastName) != null) return false;
        members[count++] = m;
        return true;
    }

    public bool Remove(string first, string last)
    {
        for (int i = 0; i < count; i++)
        {
            if (members[i].FirstName == first && members[i].LastName == last)
            {
                // Shift left
                for (int j = i; j < count - 1; j++)
                {
                    members[j] = members[j + 1];
                }
                members[--count] = null;
                return true;
            }
        }
        return false;
    }

    public Member Find(string first, string last)
    {
        for (int i = 0; i < count; i++)
        {
            if (members[i].FirstName == first && members[i].LastName == last)
            {
                return members[i];
            }
        }
        return null;
    }

    public Member[] GetAllMembers()
    {
        Member[] result = new Member[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = members[i];
        }
        return result;
    }
}
