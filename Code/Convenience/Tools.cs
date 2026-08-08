using System;
using System.Collections.Generic;
using System.Text;
using tools;
using UnityEngine;
using Zerg.Code.Content;

namespace Zerg.Code.Convenience
{
    public static class Tools
    {
        public static List<WorldTile> _temp_list_tiles = new List<WorldTile>();
        public static bool canBuildFrom(WorldTile pTile, BuildingAsset pNewBuildingAsset)
        {
            foreach(Actor actor in Finder.getUnitsFromChunk(pTile,1,3))
            {
                if(actor.asset.id == SZB.Cocoons_Building_Large || actor.asset.id == SZB.Cocoons_Building_Medium)
                {
                    return false;
                }
            }



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

        public static void throwAtTile(string id, BaseSimObject pSelf, WorldTile pTarget_tile)
        {
            Vector2Int pos = pTarget_tile.pos;
            float pDist = Vector2.Distance(pSelf.current_position, pos);
            Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, pos.x, pos.y, pDist);
            Vector3 newPoint2 = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, pos.x, pos.y, pSelf.a.stats["size"]);
            newPoint2.y += 0.25f;
            World.world.projectiles.spawn(pSelf, null, id, newPoint2, newPoint, pForcedKingdom: pSelf.kingdom);
        }



    }
}
