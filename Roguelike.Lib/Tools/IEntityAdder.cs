namespace Roguelike.Lib.Tools;

public interface IEntityAdder
{
    Dungeon InitPlayer(Dungeon dungeon, int x, int y);

    Dungeon InitMicroMouse(Dungeon dungeon);
}