namespace Roguelike.Lib.Generators;

public class RandomWalkGenerator : IWalkGenerator
{
    private static readonly Random random = new();

    public IEnumerable<(int X, int Y)> GenerateWalk()
    {
        var coverage = 20 + random.Next(16);
        var groundTilesRequired = (int)(Constants.MapWidth * Constants.MapHeight * coverage * 0.01);

        var x = 1 + random.Next(Constants.MapWidth - 2);
        var y = 1 + random.Next(Constants.MapHeight - 2);

        var dx = 0;
        var dy = 0;
        var straight = 0;

        var visited = new HashSet<(int X, int Y)>();

        while (groundTilesRequired > 0)
        {
            straight = Math.Max(0, straight - 1);
            
            if (straight == 0)
            {
                (dx, dy, straight) = random.Next(5) switch
                {
                    0 => (0, -1, straight),
                    1 => (0, 1, straight),
                    2 => (-1, 0, straight),
                    3 => (1, 0, straight),
                    _ => (0, 0, 4 + random.Next(8))
                };
            }

            x = Math.Clamp(x + dx, 1, Constants.MapWidth - 2);
            y = Math.Clamp(y + dy, 1, Constants.MapHeight - 2);

            if (!visited.Add((x, y)))
            {
                continue;
            }

            yield return (x, y);

            groundTilesRequired--;
        }
    }
}
