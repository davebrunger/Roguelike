namespace RogueLike.Inputs;

public partial class Keyboard : IKeyboard
{
    [LibraryImport("user32.dll")]
    private static partial int GetAsyncKeyState(int key);

    public bool IsKeyDown(ConsoleKey key)
    {
        return GetAsyncKeyState((int)key) > 0;
    }
}
