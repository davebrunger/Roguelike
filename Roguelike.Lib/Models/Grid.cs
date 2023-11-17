namespace Roguelike.Lib.Models;

public class Grid<T>
{
    private readonly ImmutableList<ImmutableList<T>> grid;

    private Grid(ImmutableList<ImmutableList<T>> grid)
    {
        this.grid = grid;
    }
    
    public Grid(int width, int height, Func<(int X, int Y), T> generator) : this(GenerateGrid(width, height, generator))
    {
    }

    public T this[int x, int y]
    {
        get => grid[y][x];
    }

    public Grid<T> SetItem(int x, int y, T item)
    {
        return new(grid.SetItem(y, grid[y].SetItem(x, item)));
    }

    public int Width => grid[0].Count;

    public int Height => grid.Count;

    private static ImmutableList<ImmutableList<T>> GenerateGrid(int width, int height, Func<(int X, int Y), T> generator)
    {
        return Enumerable.Range(0, height)
            .Select(y => Enumerable.Range(0, width)
                .Select(x => generator((x, y)))
                .ToImmutableList())
            .ToImmutableList();
    }
}
