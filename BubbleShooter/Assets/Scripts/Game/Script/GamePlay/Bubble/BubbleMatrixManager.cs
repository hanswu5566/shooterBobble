﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanswu.bubble
{
    public class BubbleMatrixManager : MonoBehaviour
    {
        private static BubbleMatrixManager _instance;
        private BubbleMatrixGeoInfo _geoInfo;
        private BubbleMatrix _matrix;
        private Difficulty _diffculty;

        public static BubbleMatrixManager GetInstance(int rows,int columns,BubbleMatrixGeoInfo geoInfo,Difficulty difficulty)
        {
            if (_instance == null)
            {
                var matrix = new BubbleMatrix(rows, columns);
                _instance = new BubbleMatrixManager(matrix,geoInfo, difficulty);
            }
            return _instance;  
        }

        private BubbleMatrixManager(BubbleMatrix matrix, BubbleMatrixGeoInfo geoInfo,Difficulty difficulty)
        {
            _matrix = matrix;
            _geoInfo = geoInfo;
            _diffculty = difficulty;
        }

        public BubbleMatrix GetBubbleMatrix()
        {
            return _matrix;
        }

        public BubbleMatrixGeoInfo GetBubbleMatrixGeoInfo()
        {
            return _geoInfo;
        }

        public void HandleBubbleCollision(GameObject gameObject)
        {

        }

        public bool CanBubbleMoveToPosition(Vector3 position)
        {
            Vector2 location = GetCellCoordFromPosition(position);
            if ((int)location.x <= _geoInfo.Rows - 1)
            {
                return _matrix.HasBubble((int)location.x, (int)location.y);
            }
            return true;
        }

        public Vector3 GetPositionFromCellCoord(Vector2 cell)
        {
            bool rowIsEven = cell.x % 2 == 0;
			float y = (float)(_geoInfo.TopBorder - cell.x*_geoInfo.BubbleRadius*1.8);
            float x = _geoInfo.LeftBorder + cell.y*_geoInfo.BubbleRadius*2 ;

            var offset = (float) (1* _geoInfo.BubbleRadius);

            if(rowIsEven)
            {
               x += offset;  
            }

            return new Vector3(x, y, _geoInfo.Depth);
        }

        public Vector2 GetCellCoordFromPosition(Vector3 position)
        {
            int row = _geoInfo.Rows - Mathf.FloorToInt(position.y) - 1;
			int column;
			bool rowIsEven = row% 2 == 0;
			if ((rowIsEven && _matrix.IsTopRowAlignedToLeft) || (!rowIsEven  && !_matrix.IsTopRowAlignedToLeft))
            {
				column = Mathf.FloorToInt(position.x);
			}
            else
            {
				column = Mathf.FloorToInt(position.x - _geoInfo.BubbleRadius);
			}
			Vector2 result =  new Vector2(row, Mathf.Clamp(column, 0, _geoInfo.Columns -1));
			return result;
        }

        public void HandleBubbleCollision()
        {
            //// If the ball falls under the amoun of rows, the game is over
            //Vector2 bubblePos = BubbleMatrixControllerHelper.CellForPosition(bubble.transform.position, this.geometry, this._matrix.isBaselineAlignedLeft);
            //if ((int)bubblePos.x >= this.geometry.rows)
            //{
            //    this.FinishGame(GameState.Loose);
            //    return;
            //}

            //// Create the new bubble
            //BubbleController bubbleController = bubble.GetComponent<BubbleController>();
            //Vector2 matrixPosition = BubbleMatrixControllerHelper.CellForPosition(bubble.transform.position, this.geometry, this._matrix.isBaselineAlignedLeft);

            //// Update the model
            //this._matrix.insert(bubbleController.bubble, (int)matrixPosition.x, (int)matrixPosition.y);

            //// if we don't have to add a new row (because of the timer), move the bubble smoothly to its snapping point			
            //if (!this._pendingToAddRow)
            //{
            //    bubbleController.moveTo(BubbleMatrixControllerHelper.PositionForCell(matrixPosition, geometry, this._matrix.isBaselineAlignedLeft), 0.1f);
            //}
            //else
            //{
            //    // otherwise move it rapidly
            //    bubbleController.transform.position = BubbleMatrixControllerHelper.PositionForCell(matrixPosition, geometry, this._matrix.isBaselineAlignedLeft);
            //}

            //// Explode the bubbles that need to explode
            //// The the cluster of bubbles with a similar color as the colliding one
            //ArrayList cluster = this._matrix.colorCluster(bubbleController.bubble);

            //if (cluster.Count > 2)
            //{
            //    // Explode the cluster
            //    bubbleController.transform.position = BubbleMatrixControllerHelper.PositionForCell(matrixPosition, geometry, this._matrix.isBaselineAlignedLeft);
            //    this.destroyCluster(cluster, true);
            //    // Notify that bubbles have been removed
            //    GameEvents.BubblesRemoved(cluster.Count, true);
            //}

            //// Drop the bubbles that fall
            //cluster = this._matrix.looseBubbles();
            //this.destroyCluster(cluster, false);
            //if (cluster.Count > 0)
            //    GameEvents.BubblesRemoved(cluster.Count, false);

            //// Add a new Row of random bubbles if required
            //if (_pendingToAddRow)
            //{
            //    this.addRow();
            //    StartCoroutine("addRowScheduler");
            //}

            //// If there are no bubble lefts, win the game
            //if (this._matrix.bubbles.Count == 0)
            //{
            //    this.FinishGame(GameState.Win);
            //    return;
            //}

            //// Prepare the new bubble to shoot it
            //this._currentBubble = this.createBubble();
        }
    }
}