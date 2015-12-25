using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using Engine.I18N;
using Engine.Objects;

namespace Engine.EGUI.Inventory {
	
	public class ItemDrawService {
		
		private const float shadowSize = 2f;

		private GUIStyle labelStyle  = null;
		private GUIStyle labelShadow = null;
		//private Color    labelColor = new Color(0.945f, 0.768f, 0.058f);

			public ItemDrawService(GUIStyle labelStyle, GUIStyle labelShadow){
				this.labelStyle=labelStyle;
				this.labelShadow=labelShadow;
			}
		
		/// <summary>
		/// ��������� �������� � ���������
		/// </summary>
		/// <param name="item">�������� �������</param>
		/// <param name="offsetX">�������� (�������) ��������� �� x</param>
		/// <param name="offsetY">�������� (�������) ��������� �� y</param>
		/// <param name="fixWebPosition">��� fixWebPosition=true ������� �������� "� �����"</param>
		/// <param name="drawIcon">��� drawIcon=false, ������� �������� ��� ������</param>
		public void DrawItem(ItemSlot item, float offsetX, float offsetY, bool fixWebPosition = true, bool drawIcon = true){
			
			Rect cellRectangle;

			if (fixWebPosition) {

				cellRectangle = new Rect(offsetX + CellSettings.cellPaddingX + (item.position.X - 1) * CellSettings.cellWidth+shadowSize,
										 offsetY + CellSettings.cellPaddingY + (item.position.Y - 1) * CellSettings.cellHeight+shadowSize,
										 CellSettings.cellWidth,
										 CellSettings.cellHeight);

			} else {

				cellRectangle = new Rect(offsetX+shadowSize,
										 offsetY+shadowSize,
										 CellSettings.cellWidth,
										 CellSettings.cellHeight);

			}

			string description = item.item.getCount()>1? item.item.getCount().ToString()+CLang.getInstance().get(Dictionary.K_COUNT) : "";

			GUI.Box(cellRectangle, description, labelShadow);

			cellRectangle.x-=shadowSize;
			cellRectangle.y-=shadowSize;

			if (drawIcon)
				labelStyle.normal.background = item.item.resource.icon;

			GUI.color = new Color(1f,1f,1f,1f);
			GUI.Box(cellRectangle, description, labelStyle);
			
		}
		
		
	}
	
}