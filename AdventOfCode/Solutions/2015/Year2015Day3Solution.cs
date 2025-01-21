namespace AdventOfCode.Solutions._2015;
internal class Year2015Day3Solution : IDaySolution
{
    public string GetFirstPartAnswer(string inputData)
    {
        var visitsByCoord = new Dictionary<string, int>
        {
            {
              "0,0", 1
            }
        };

        var santaXCoord = 0;
        var santaYCoord = 0;

        foreach (var movementCh in inputData)
        {
            MoveSanta(ref santaXCoord, ref santaYCoord, movementCh);

            var currentSantaCoords = $"{santaXCoord},{santaYCoord}";

            visitsByCoord.TryAdd(currentSantaCoords, 0);
            visitsByCoord[currentSantaCoords]++;
        }

        return visitsByCoord.Count().ToString();
    }

    public string GetSecondPartAnswer(string inputData)
    {
        var visitsByCoord = new Dictionary<string, int>
        {
            {
              "0,0", 2
            }
        };

        var santaXCoord = 0;
        var santaYCoord = 0;

        var roboSantaXCoord = 0;
        var roboSantaYCoord = 0;

        for (int i = 0; i < inputData.Length; i++)
        {
            int currentSantaXCoord;
            int currentSantaYCoord;
            if (i % 2 == 0)
            {
                MoveSanta(ref santaXCoord, ref santaYCoord, inputData[i]);

                currentSantaXCoord = santaXCoord;
                currentSantaYCoord = santaYCoord;
            }
            else
            {
                MoveSanta(ref roboSantaXCoord, ref roboSantaYCoord, inputData[i]);

                currentSantaXCoord = roboSantaXCoord;
                currentSantaYCoord = roboSantaYCoord;
            }


            var currentSantaCoords = $"{currentSantaXCoord},{currentSantaYCoord}";

            visitsByCoord.TryAdd(currentSantaCoords, 0);
            visitsByCoord[currentSantaCoords]++;
        }
        return visitsByCoord.Count().ToString();
    }

    private void MoveSanta(ref int santaXCoord, ref int santaYCoord, char movementCh)
    {
        switch (movementCh)
        {
            case '^':
                santaYCoord++;
                break;
            case 'v':
                santaYCoord--;
                break;
            case '>':
                santaXCoord++;
                break;
            case '<':
                santaXCoord--;
                break;
        }
    }
}
