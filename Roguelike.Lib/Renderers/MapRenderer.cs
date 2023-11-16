namespace Roguelike.Lib.Renderers
{
    public class MapRenderer
    {
        private readonly ITileRenderer tileRenderer;

        public MapRenderer(ITileRenderer tileRenderer)
        {
            this.tileRenderer = tileRenderer;
        }

        public void RenderDungeon(Dungeon dungeon)
        {
            tileRenderer.Clear();
            for (var y = 0; y < Constants.RenderHeight; y++)
            {
                for (var x = 0; x < Constants.RenderWidth; x++)
                {
                    var mx = x + dungeon.Camera.X;
                    var my = y + dungeon.Camera.Y;

                    if (mx >= 0 && my >= 0 && mx < Constants.MapWidth && my < Constants.MapHeight)
                    {
                        var tile = dungeon.MapTiles[my][mx];
                        tileRenderer.Render(mx, my, tile);
                    }
                }
                tileRenderer.NewLine();
            }
        }
    }
}
