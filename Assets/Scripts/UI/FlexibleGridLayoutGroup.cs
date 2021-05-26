using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.UI.Ext
{
    public enum FitType
    {
        Uniform,
        FixRows,
        FixColumns,
        Width,
        Height,
    }

    public class FlexibleGridLayoutGroup : LayoutGroup
    {
        public Vector2 cellSize;
        public Vector2 spacing;

        public FitType fitType;

        public int rows;
        public int columns;
        public bool fixX;
        public bool fixY;


        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            if (fitType == FitType.Width ||
                fitType == FitType.Height ||
                fitType == FitType.Uniform)
            {
                fixX = fixY = true;
                float sqrRt = Mathf.Sqrt(transform.childCount);
                rows = sqrRt != 0 ? Mathf.CeilToInt(sqrRt) : 1;
                columns = sqrRt != 0 ? Mathf.CeilToInt(sqrRt) : 1;
            }

            if (fitType == FitType.Width || fitType == FitType.FixColumns)
            {
                rows = transform.childCount != 0 ? Mathf.CeilToInt(transform.childCount / columns) : 1;
            }
            else
            if (fitType == FitType.Height || fitType == FitType.FixRows)
            {
                columns = transform.childCount != 0 ? Mathf.CeilToInt(transform.childCount / rows) : 1;
            }

            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            float cellWidth = (parentWidth / columns) - ((spacing.x / columns) * 2) - (padding.left / columns) - (padding.right / columns);
            float cellHeight = (parentHeight / rows) - ((spacing.y / rows) * 2) - (padding.top / rows) - (padding.bottom / rows);

            cellSize.x = fixX ? cellWidth : cellSize.x;
            cellSize.y = fixY ? cellHeight : cellSize.y;

            int rowCount;
            int columnCount;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                rowCount = i / columns;
                columnCount = i % columns;

                var item = rectChildren[i];

                var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
                var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }
        }
        public override void CalculateLayoutInputVertical()
        {
            //throw new System.NotImplementedException();
        }
        public override void SetLayoutHorizontal()
        {
            //throw new System.NotImplementedException();
        }
        public override void SetLayoutVertical()
        {
            //throw new System.NotImplementedException();
        }
    }
}





