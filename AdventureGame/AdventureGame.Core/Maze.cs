namespace AdventureGame.Core;

public class Maze
{
    public int width { get; set; }
    public int height { get; set; }
    public char symbol { get; set; }

    public char[,] tiles { get; set; }

    Random random = new();

    public Maze(int width, int height)
    {
        this.width = width;
        this.height = height;
        tiles = new char[width, height];

        CreateTiles();
        PlaceOutsideWalls();
    }

    public Maze(int width, int height, char symbol, char[,] tiles, Random random) : this(width, height)
    {
        this.symbol = symbol;
        this.tiles = tiles;
        this.random = random;
    }

    public void CreateTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = '.';
            }
        }
    }

    public void PlaceOutsideWalls()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || y == 0 || x == (width - 1) || y == (height - 1))
                    tiles[x, y] = '#';
            }
        }
    }

    public void PlacePlayer(Cat_Player cat)
    {
        tiles[cat.xPos, cat.yPos] = '@';
    }

    public void PlaceExit()
    {
        while (true)
        {
            int randomRow = random.Next(1, height - 1);
            int randomCol = random.Next(1, width - 1);

            if (tiles[randomRow, randomCol] == '.')
            {
                tiles[randomRow, randomCol] = 'E';
                break;
            }
        }
    }

    public void PlaceMonsterDog()
    {
        while (true)
        {
            int randomRow = random.Next(1, height - 1);
            int randomCol = random.Next(1, width - 1);

            if (tiles[randomRow, randomCol] == '.')
            {
                tiles[randomRow, randomCol] = 'M';
                break;
            }


        }

    }
    public void PlaceInsideWalls()
    {
        int wallCount = 0;
        while (wallCount < 5)
        {
            int randomRow = random.Next(1, height - 1);
            int randomCol = random.Next(1, width - 1);
            if (tiles[randomRow, randomCol] == '.' && CheckAround(randomRow, randomCol) == true)
            {
                tiles[randomRow, randomCol] = '#';
                wallCount++;
            }
        }

    }
    public bool CheckAround(int row, int column)
    {
        int numOccupied = 0;
        int threshold = 3;

        if (tiles[row + 1, column] != '.')
            numOccupied++;
        if (tiles[row, column + 1] != '.')
            numOccupied++;
        if (tiles[row - 1, column] != '.')
            numOccupied++;
        if (tiles[row, column - 1] != '.')
            numOccupied++;

        return (numOccupied >= threshold) ? false : true;

    }

    public void PlaceMilkPotions()
    {
        while (true)
        {
            int randomRow = random.Next(1, height - 1);
            int randomCol = random.Next(1, width - 1);
            if (tiles[randomRow, randomCol] == '.')
            {
                tiles[randomRow, randomCol] = 'P';
                break;
            }
        }
    }
    public void PlaceWeapons()
    {
        while (true)
        {
            int randomRow = random.Next(1, height - 1);
            int randomCol = random.Next(1, width - 1);
            if (tiles[randomRow, randomCol] == '.')
            {
                tiles[randomRow, randomCol] = 'W';
                break;
            }
        }
    }
}
    
