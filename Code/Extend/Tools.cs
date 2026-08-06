using System;
using System.Collections.Generic;
using System.Text;
using tools;
using Zerg.Code.Content;

namespace Zerg.Code.Extend
{
    public static class Tools
    {
        public static List<WorldTile> _temp_list_tiles = new List<WorldTile>();
        public static bool canBuildFrom(WorldTile pTile, BuildingAsset pNewBuildingAsset)
        {

            BuildingFundament fundament = pNewBuildingAsset.fundament;
            int num = pTile.x - fundament.left;
            int num2 = pTile.y - fundament.bottom;
            int width = fundament.width;
            int height = fundament.height;
            List<WorldTile> temp_list_tiles = _temp_list_tiles;
            temp_list_tiles.Clear();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    WorldTile tile = World.world.GetTile(num + i, num2 + j);
                    if (tile == null)
                    {
                        return false;
                    }

                    temp_list_tiles.Add(tile);
                    Building building = tile.building;
                    TileTypeBase type = tile.Type;

                    


                    if (type.liquid && !pNewBuildingAsset.can_be_placed_on_liquid)
                    {
                        return false;
                    }

                    if (pNewBuildingAsset.destroy_on_liquid && type.ocean)
                    {
                        return false;
                    }

                    if (!tile.canBuildOn(pNewBuildingAsset))
                    {
                        return false;
                    }

                    if (i == 0)
                    {
                        if (isBuildingNearby(tile.tile_left))
                        {
                            return false;
                        }
                    }
                    else if (i == width - 1 && isBuildingNearby(tile.tile_right))
                    {
                        return false;
                    }

                    if (j == 0)
                    {
                        if (isBuildingNearby(tile.tile_down))
                        {
                            return false;
                        }

                        if (tile.has_tile_down && isBuildingNearby(tile.tile_down.tile_down))
                        {
                            return false;
                        }
                    }
                    else if (j == height - 1)
                    {
                        if (isBuildingNearby(tile.tile_up))
                        {
                            return false;
                        }

                        if (tile.has_tile_up && isBuildingNearby(tile.tile_up.tile_up))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private static bool isBuildingNearby(WorldTile pTile)
        {
            if (pTile == null)
            {
                return true;
            }
            Building tBuilding = pTile.building;
            return tBuilding != null && tBuilding.isUsable() && (tBuilding.asset.city_building || ZergBuilding.list.Contains(tBuilding.asset.id));
        }


    }
}
