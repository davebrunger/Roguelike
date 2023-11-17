namespace Roguelike.Lib.Generators;

public interface IWalkGenerator
{
    IEnumerable<(int X, int Y)> GenerateWalk();
}