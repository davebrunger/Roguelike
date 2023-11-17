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

            var cameraX = dungeon.Entities[0].X - (Constants.RenderWidth / 2);
            var cameraY = dungeon.Entities[0].Y - (Constants.RenderHeight / 2);

            for (var y = 0; y < Constants.RenderHeight; y++)
            {
                for (var x = 0; x < Constants.RenderWidth; x++)
                {
                    var mx = x + cameraX;
                    var my = y + cameraY;

                    if (mx >= 0 && my >= 0 && mx < dungeon.Tiles.Width && my < dungeon.Tiles.Height)
                    {
                        var tile = dungeon.Tiles[mx, my];
                        var entities = dungeon.Entities.Where(e => e.X == mx && e.Y == my).ToList();
                        tileRenderer.Render(x, y, tile, entities);
                    }
                    else
                    {
                        tileRenderer.Render(x, y, new Tile(TileType.Hole, false, false), Enumerable.Empty<Entity>());
                    }
                }
                tileRenderer.NewLine();
            }
        }
    }
}
