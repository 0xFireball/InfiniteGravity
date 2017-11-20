using Nez.Tiled;

namespace InvisiblePlanet.Util {
    public class WallMapGenerator {
        public static TiledTile[] generateWallMap(int width, int height, TiledTileset tileset) {
            var map = new TiledTile[width * height];
            var wallTile = 0;

            for (var r = 0; r < height; r++) {
                for (var c = 0; c < width; c++) {
                    if (r == 0 || r == height - 1) {
                        // map.push(wallTile);
                        map[r * width + c] = new TiledTile(wallTile);
                    } else {
                        if (c == 0 || c == (width - 1)) {
                            //map.push(wallTile);
                            map[r * width + c] = new TiledTile(wallTile);
                        } else {
                            //map.push(airTile);
                            map[r * width + c] = null;
                        }
                    }
                    if (map[r * width + c] != null) {
                        map[r * width + c].tileset = tileset;
                    }
                }
            }
            return map;
        }
    }
}