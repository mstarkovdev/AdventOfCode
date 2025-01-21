namespace AdventOfCode.Solutions._2015;
internal class Year2015Day5Solution : IDaySolution
{
    public string GetFirstPartAnswer(string inputData)
    {
        var niceStringCount = 0;

        var strings = inputData.Split("\n");
        foreach (var str in strings)
        {
            if (CheckIfStringIsNiceP1(str))
            {
                niceStringCount++;
            }
        }

        return niceStringCount.ToString();
    }

    public string GetSecondPartAnswer(string inputData)
    {
        var niceStringCount = 0;

        var strings = inputData.Split("\n");
        foreach (var str in strings)
        {
            if (CheckIfStringIsNiceP2(str))
            {
                niceStringCount++;
            }
        }

        return niceStringCount.ToString();
    }

    public bool CheckIfStringIsNiceP1(string strToCheck)
    {
        var vowels = new HashSet<char>
        {
            'a', 'e', 'i', 'o', 'u'
        };

        var forbiddenStrings = new HashSet<string>
        {
            "ab", "cd", "pq", "xy"
        };

        var vowelCount = 0;
        bool hasDoubleLetter = false;
        char? prevCh = null;

        foreach (var ch in strToCheck)
        {
            if (vowels.Contains(ch))
            {
                vowelCount++;
            }
            if (prevCh == null)
            {
                prevCh = ch;
                continue;
            }
            if (prevCh == ch)
            {
                hasDoubleLetter = true;
            }
            if (forbiddenStrings.Contains($"{prevCh}{ch}"))
            {
                return false;
            }
            prevCh = ch;
        }

        return vowelCount >= 3 && hasDoubleLetter;
    }

    public bool CheckIfStringIsNiceP2(string strToCheck)
    {
        var hasPairOfTwoLetters = false;
        var hasRepeatingLetterWithOneLetterBetween = false;
        for (int i = 0; i < strToCheck.Length - 1; i++)
        {
            if (i + 2 < strToCheck.Length && strToCheck[i] == strToCheck[i + 2])
            {
                hasRepeatingLetterWithOneLetterBetween = true;
            }
            if (strToCheck.Substring(i + 2).Contains(strToCheck.Substring(i, 2)))
            {
                hasPairOfTwoLetters = true;
            }
        }
        return hasPairOfTwoLetters && hasRepeatingLetterWithOneLetterBetween;
    }
}
