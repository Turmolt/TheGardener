using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Zenject;

namespace BackwardsCap
{
    public class MapManager : MonoBehaviour
    {

        [Inject] private Tilemap map;
        [Inject(Id="Object Tilemap")] private Tilemap objectTilemap;

        [SerializeField] private TileBase hole;
        [SerializeField] private TileBase planted;

        
        #region Limbs

        public bool PlantLimb(Vector2 pos)
        {
            if (CheckTile(pos,objectTilemap,"Hole"))
            {
                objectTilemap.SetTile(Vector3Int.RoundToInt(pos),planted);
                return true;
            }

            return false;
        }
        
        public bool PlantEye(Vector2 pos)
        {
            if (CheckTile(pos,map,"Blood")&&!CheckTile(pos,objectTilemap))
            {
                //objectTilemap.SetTile(Vector3Int.RoundToInt(pos),planted);
                return true;
            }

            return false;
        }

        #endregion
        
        #region Shovel
        public void DigHole(Vector2 pos)
        {
            //check that there is no object on this tile, also that it is a "dirt" type
            if(CheckTile(pos,map,"Dirt")&&!CheckTile(pos,objectTilemap))
            {
                objectTilemap.SetTile(Vector3Int.RoundToInt(pos),hole);
            }
        }
        
        #endregion

        private bool CheckTile(Vector2 pos, Tilemap tilemap, string tileType=null)
        {
            bool result = false;
            var p = Vector3Int.RoundToInt(pos);
            var t = tilemap.GetTile(p);

            if (tileType != null)
            {
                return t != null && t.name.ToLower().Contains(tileType.ToLower());
            }
            return t!=null;
        }
    }
}