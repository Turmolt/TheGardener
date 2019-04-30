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

        [SerializeField] private TileBase dirt;
        [SerializeField] private TileBase hole;
        [SerializeField] private TileBase planted;
        [SerializeField] private TileBase watered;
        [SerializeField] private TileBase blood;
        [SerializeField] private TileBase bloodPlanted;
        [SerializeField] private TileBase bloodWatered;

        
        #region Limbs
        public bool PlantLimb(Vector2 pos)
        {
            if (CheckTile(pos,objectTilemap,hole))
            {
                SetTile(objectTilemap, pos, planted);
                return true;
            }

            return false;
        }
        
        public bool PlantEye(Vector2 pos)
        {
            if (CheckTile(pos,map,blood)&&!CheckTile(pos,objectTilemap))
            {
                objectTilemap.SetTile(Vector3Int.RoundToInt(pos),bloodPlanted);
                return true;
            }

            return false;
        }

        #endregion
        
        #region Tools

        public void HarvestSpot(Vector2 pos)
        {
            if (CheckTile(pos, objectTilemap, planted) || CheckTile(pos, objectTilemap, watered)||CheckTile(pos, objectTilemap, bloodPlanted)||CheckTile(pos, objectTilemap, bloodWatered))
            {
                SetTile(objectTilemap,pos,null);
            }

        }
        
        public bool WaterArea(Vector2 pos)
        {
            if (CheckTile(pos, objectTilemap, planted))
            {
                SetTile(objectTilemap,pos,watered);
                return true;
            }
            else if (CheckTile(pos, objectTilemap, bloodPlanted))
            {
                SetTile(objectTilemap,pos,bloodWatered);
                return true;
            }

            return false;
        }
        
        public bool DigHole(Vector2 pos)
        {
            //check that there is no object on this tile, also that it is a "dirt" type
            if(CheckTile(pos,map,dirt)&&!CheckTile(pos,objectTilemap))
            {
                objectTilemap.SetTile(Vector3Int.RoundToInt(pos),hole);
                return true;
            }

            return false;
        }

        public void SetDirt(Vector2 pos)
        {
            SetTile(map,pos,dirt);
        }
        
        #endregion

        public bool CheckPlant(Vector2 pos, bool removeWater=false)
        {
            var watered = CheckTile(pos, objectTilemap,this.watered);
            if (watered && removeWater) SetTile(objectTilemap,pos,planted);
            if (!watered) watered = CheckTile(pos, objectTilemap, this.bloodWatered);
            if (watered && removeWater) SetTile(objectTilemap, pos, bloodPlanted);
            return watered;
        }


        private void SetTile(Tilemap tilemap, Vector2 pos, TileBase newTile)
        {
            tilemap.SetTile(Vector3Int.RoundToInt(pos),newTile);
        }

        private bool CheckTile(Vector2 pos, Tilemap tilemap, TileBase tileType=null)
        {
            bool result = false;
            var p = Vector3Int.RoundToInt(pos);
            var t = tilemap.GetTile(p);

            if (tileType != null)
            {
                return t != null && t.name == tileType.name;
            }
            return t!=null;
        }
    }
}