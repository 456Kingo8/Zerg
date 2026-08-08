using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.CanvasScaler;

namespace Zerg.Code.Content
{
    class ZergProjectile
    {
        public static void init()
        {
            ProjectileAsset asset = AssetManager.projectiles.clone("zerg_spine", "arrow");
            asset.mass = 1;
            asset.speed = 60;


            asset = new ProjectileAsset();
            asset.id = "glaive_wurm_0";//这是会弹跳三次的刃虫，你能这段代码对视10s不笑吗
            asset.animated = true;
            asset.texture_shadow = "shadows/projectiles/shadow_ball";
            asset.look_at_target = true;
            asset.speed = 25;
            asset.texture = "Glaive_Wurm";
            asset.animation_speed = 60f;
            asset.mass = 1f;
            asset.scale_start = 0.07f;
            asset.scale_target = 0.07f;
            asset.draw_light_area = false;
            asset.world_actions = bounce_1;
            AssetManager.projectiles.add(asset);

            asset = AssetManager.projectiles.clone("glaive_wurm_1", "glaive_wurm_0");
            asset.world_actions = bounce_2;
            asset = AssetManager.projectiles.clone("glaive_wurm_2", "glaive_wurm_0");
            asset.world_actions = null;

        }

        private static bool bounce_1(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pSelf != null && pTile != null && pSelf.kingdom != null)
            {
                BaseSimObject target = null;
                foreach (BaseSimObject obj in Finder.getAllObjectsInChunks(pTile, 10))
                {
                    if (obj != null && obj.kingdom != null && obj.kingdom.isEnemy(pSelf.kingdom))
                    {
                        if (Vector2.Distance(pTile.pos, obj.current_position) < 0.2) continue;
                        target = obj;
                        break;
                    }
                }
                if (target != null)
                {
                    Vector2 pos = target.current_position;
                    float pDist = Vector2.Distance(pTile.pos, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pTile.pos.x, pTile.pos.y, pos.x, pos.y, pDist);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTile.pos.x, pTile.pos.y, pos.x, pos.y, target.stats["size"]);
                    newPoint2.y += 1f;
                    World.world.projectiles.spawn(pSelf, target, "glaive_wurm_1", newPoint2, newPoint, target.getHeight(), pForcedKingdom: pSelf.kingdom);
                }
            }
            return true;
        }

        private static bool bounce_2(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pSelf != null && pTile != null && pSelf.kingdom != null)
            {
                BaseSimObject target = null;
                foreach (BaseSimObject obj in Finder.getAllObjectsInChunks(pTile, 9))
                {
                    if (obj != null && obj.kingdom != null && obj.kingdom.isEnemy(pSelf.kingdom))
                    {
                        target = obj;
                        break;
                    }
                }
                if (target != null)
                {
                    Vector2 pos = target.current_position;
                    float pDist = Vector2.Distance(pTile.pos, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pTile.pos.x, pTile.pos.y, pos.x, pos.y, pDist);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTile.pos.x, pTile.pos.y, pos.x, pos.y, target.stats["size"]);
                    newPoint2.y += 0.5f;
                    World.world.projectiles.spawn(pSelf, target, "glaive_wurm_2", newPoint2, newPoint, pForcedKingdom: pSelf.kingdom);
                }
            }
            return true;
        }
    }
}
