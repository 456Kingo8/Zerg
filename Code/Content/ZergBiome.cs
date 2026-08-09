using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Zerg.Code.Content
{
    class ZergBiome
    {
        public static void init()
        {
            BiomeAsset biome = new BiomeAsset();
            biome.id = "biome_zerg_creep";
            biome.tile_high = "zerg_creep_high";
            biome.tile_low = "zerg_creep_low";
            biome.localized_key = "zerg_creep";
            AssetManager.biome_library.add(biome);





            TopTileType tile = AssetManager.top_tiles.clone("zerg_creep_low", "tumor_low");
            tile.drawPixel = true;
            tile.height_min = 98;
            tile.color = new Color32(61, 57, 56, 230);
            tile.walk_multiplier = 0.5f;
            tile.ignore_walk_multiplier_if_tag = "Zerg";
            tile.only_allowed_to_build_with_tag = "Zerg";
            tile.layer_type = TileLayerType.Ground;
            tile.additional_height = null;
            tile.fire_chance = 0.02f;
            tile.biome_id = biome.id;
            tile.biome_asset = biome;
            tile.remove_on_freeze = false;
            tile.remove_on_heat = true;
            tile.can_be_frozen = true;
            tile.force_edge_variation = true;
            tile.force_edge_variation_frame = 1;
            tile.rank_type = TileRank.Low;
            tile.setDrawLayer(TileZIndexes.tumor_low, null);
            tile.step_action_chance = 1f;
            tile.step_action = zerg_creep;

            tile = AssetManager.top_tiles.clone("zerg_creep_high", "zerg_creep_low");
            tile.height_min = 108;
            tile.walk_multiplier = 0.6f;
            tile.color = new Color32(61, 57, 56, 245);
            tile.remove_on_freeze = false;
            tile.remove_on_heat = true;
            tile.can_be_frozen = true;
            tile.force_edge_variation = true;
            tile.force_edge_variation_frame = 1;
            tile.rank_type = TileRank.High;
            tile.setDrawLayer(TileZIndexes.tumor_high, null);
            tile.biome_id = biome.id;
            tile.biome_asset = biome;
            tile.step_action_chance = 1f;
            tile.step_action = zerg_creep;
            tile.ignore_walk_multiplier_if_tag = "Zerg";
            tile.only_allowed_to_build_with_tag = "Zerg";


            BuildRuntimeTileAtlas();
            if (ZergMain.I.GetConfig()["Texture Config"]["Zerg_Creep_Texture_Dark_Side"].BoolVal)
            {
                loadSprite(AssetManager.top_tiles.get("zerg_creep_low"), "zerg_creep_low_dark");
                loadSprite(AssetManager.top_tiles.get("zerg_creep_high"), "zerg_creep_high_dark");
            }
            else
            {
                loadSprite(AssetManager.top_tiles.get("zerg_creep_low"));
                loadSprite(AssetManager.top_tiles.get("zerg_creep_high"));
            }
        }

        public static bool zerg_creep(WorldTile pTile, Actor pActor)
        {
            if (pActor.hasTag("Zerg")||pActor.hasTrait("Zerg"))
            {
                pActor.addStatusEffect("zerg_creep",pColorEffect: false);
            }
            return true;

        }


        //以下代码来自启源修仙 赞美一米！
        //https://github.com/inmny/Cultiway-Reborn/blob/master/Source/Content/TopTileTypes.cs

        private const int RuntimeAtlasPadding = 3;

        private static Dictionary<string, Sprite[]> _runtimeTileSpritesByAssetId;


        private static void BuildRuntimeTileAtlas()
        {
            _runtimeTileSpritesByAssetId = new Dictionary<string, Sprite[]>(StringComparer.OrdinalIgnoreCase);
            string tilesRoot = Path.Combine(ZergMain.I.GetDeclaration().FolderPath, "GameResources", "tiles");
            if (!Directory.Exists(tilesRoot)) return;

            List<TileSpriteSource> sources = LoadTileSpriteSources(tilesRoot);
            if (sources.Count == 0) return;

            int maxWidth = sources.Max(source => source.Width);
            int maxHeight = sources.Max(source => source.Height);
            int cellWidth = maxWidth + RuntimeAtlasPadding * 2;
            int cellHeight = maxHeight + RuntimeAtlasPadding * 2;
            int columns = Mathf.CeilToInt(Mathf.Sqrt(sources.Count));
            int rows = Mathf.CeilToInt((float)sources.Count / columns);
            int atlasWidth = Mathf.NextPowerOfTwo(columns * cellWidth);
            int atlasHeight = Mathf.NextPowerOfTwo(rows * cellHeight);
            Color32[] atlasPixels = new Color32[atlasWidth * atlasHeight];

            Texture2D atlasTexture = new Texture2D(atlasWidth, atlasHeight, TextureFormat.RGBA32, true)
            {
                name = "Zerg_TileRuntimeAtlas",
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp,
                anisoLevel = 10
            };

            Dictionary<string, List<Sprite>> spritesByAssetId =
                new Dictionary<string, List<Sprite>>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < sources.Count; i++)
            {
                TileSpriteSource source = sources[i];
                int cellX = i % columns;
                int cellY = i / columns;
                int atlasX = cellX * cellWidth;
                int atlasY = cellY * cellHeight;
                int spriteX = atlasX + RuntimeAtlasPadding;
                int spriteY = atlasY + RuntimeAtlasPadding;

                CopySpriteToAtlas(source, atlasPixels, atlasWidth, spriteX, spriteY);

                Sprite sprite = Sprite.Create(
                    atlasTexture,
                    new Rect(spriteX, spriteY, source.Width, source.Height),
                    new Vector2(0.5f, 0.5f),
                    1f,
                    (uint)RuntimeAtlasPadding,
                    SpriteMeshType.FullRect,
                    Vector4.zero);
                sprite.name = source.Name;

                if (!spritesByAssetId.TryGetValue(source.AssetId, out List<Sprite> list))
                {
                    list = new List<Sprite>();
                    spritesByAssetId[source.AssetId] = list;
                }
                list.Add(sprite);
            }

            atlasTexture.SetPixels32(atlasPixels);
            atlasTexture.Apply(updateMipmaps: true, makeNoLongerReadable: false);

            foreach (KeyValuePair<string, List<Sprite>> pair in spritesByAssetId)
            {
                _runtimeTileSpritesByAssetId[pair.Key] = pair.Value.ToArray();
            }

            ZergMain.LogInfo($"[TopTileTypes] Loaded {sources.Count} tile sprites into runtime atlas {atlasWidth}x{atlasHeight}");
        }

        private static List<TileSpriteSource> LoadTileSpriteSources(string tilesRoot)
        {
            List<TileSpriteSource> sources = new List<TileSpriteSource>();
            foreach (string directory in Directory.GetDirectories(tilesRoot).OrderBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase))
            {
                string assetId = Path.GetFileName(directory);
                string[] files = Directory.GetFiles(directory, "*.png")
                    .OrderBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase)
                    .ToArray();
                foreach (string file in files)
                {
                    Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, true)
                    {
                        filterMode = FilterMode.Point,
                        wrapMode = TextureWrapMode.Clamp
                    };
                    if (!ImageConversion.LoadImage(texture, File.ReadAllBytes(file)))
                    {
                        UnityEngine.Object.Destroy(texture);
                        continue;
                    }

                    sources.Add(new TileSpriteSource
                    {
                        AssetId = assetId,
                        Name = Path.GetFileNameWithoutExtension(file),
                        Width = texture.width,
                        Height = texture.height,
                        Pixels = texture.GetPixels32()
                    });
                    UnityEngine.Object.Destroy(texture);
                }
            }

            return sources;
        }

        private static void CopySpriteToAtlas(TileSpriteSource source, Color32[] atlasPixels, int atlasWidth, int spriteX, int spriteY)
        {
            for (int y = -RuntimeAtlasPadding; y < source.Height + RuntimeAtlasPadding; y++)
            {
                int sourceY = Mathf.Clamp(y, 0, source.Height - 1);
                int targetY = spriteY + y;
                for (int x = -RuntimeAtlasPadding; x < source.Width + RuntimeAtlasPadding; x++)
                {
                    int sourceX = Mathf.Clamp(x, 0, source.Width - 1);
                    int targetX = spriteX + x;
                    atlasPixels[targetY * atlasWidth + targetX] = source.Pixels[sourceY * source.Width + sourceX];
                }
            }
        }

        private class TileSpriteSource
        {
            public string AssetId;
            public string Name;
            public int Width;
            public int Height;
            public Color32[] Pixels;
        }

        private static void loadSprite(TopTileType asset)
        {
            Sprite[] tSpritesArr = _runtimeTileSpritesByAssetId.TryGetValue(asset.id, out Sprite[] sprites) ? sprites : null;
            if (tSpritesArr?.Length > 0)
            {
                asset.sprites = new TileSprites();
                foreach (Sprite tSprite in tSpritesArr)
                {
                    asset.sprites.addVariation(tSprite, asset.id);
                }
            }
            World.world.tilemap.createTileMapFor(asset);
        }

        private static void loadSprite(TopTileType asset,string id)
        {
            Sprite[] tSpritesArr = _runtimeTileSpritesByAssetId.TryGetValue(id, out Sprite[] sprites) ? sprites : null;
            if (tSpritesArr?.Length > 0)
            {
                asset.sprites = new TileSprites();
                foreach (Sprite tSprite in tSpritesArr)
                {
                    asset.sprites.addVariation(tSprite, asset.id);
                }
            }
            World.world.tilemap.createTileMapFor(asset);
        }
    }
}
