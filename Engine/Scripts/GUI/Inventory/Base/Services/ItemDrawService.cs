using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using Engine.I18N;
using Engine.Objects;

namespace Engine.EGUI.Inventory {
	
	public class ItemDrawService {
		
		private static GUIStyle labelStyle = null;
		private static Color    labelColor = new Color(0.945f, 0.768f, 0.058f);

			public ItemDrawService(){
			
			}
		
		private static GUIStyle getLabelStyle() {

			if (labelStyle==null) {
				labelStyle = new GUIStyle(GUI.skin.label);
				labelStyle.alignment = TextAnchor.MiddleRight;
				labelStyle.normal.textColor = labelColor;
				labelStyle.fontSize = 15;
				labelStyle.fontStyle = FontStyle.Bold;
			}

			return labelStyle;

		}
		
		/// <summary>
		/// Отрисовка предмета в инвентаре
		/// </summary>
		/// <param name="item">Рисуемый предмет</param>
		/// <param name="offsetX">Смещение (позиция) инвентаря по x</param>
		/// <param name="offsetY">Смещение (позиция) инвентаря по y</param>
		/// <param name="fixWebPosition">при fixWebPosition=true предмет рисуется "в сетке"</param>
		/// <param name="drawIcon">при drawIcon=false, предмет рисуется без иконки</param>
		public void DrawItem(ItemSlot item, float offsetX, float offsetY, bool fixWebPosition = true, bool drawIcon = true){
			
			if(fixWebPosition) {
				
				Rect cellRectangle = new Rect(offsetX+CellSettings.cellPaddingX+(item.position.X-1)*CellSettings.cellWidth,
											  offsetY+CellSettings.cellPaddingY+(item.position.Y-1)*CellSettings.cellHeight,
											  (float)CellSettings.cellWidth,
											  (float)CellSettings.cellHeight);
				//if (drawIcon)
				//GUI.DrawTexture(cellRectangle, resource.icon);

				if(item.item.getCount()>1){

					Rect labelRectangle = new Rect(cellRectangle.x+4,
												   cellRectangle.y+cellRectangle.height*item.item.getSize().getHeight()-22.0f,
												   cellRectangle.width*item.item.getSize().getWidth()-8,
												   20.0f);

					GUI.Label(labelRectangle, item.item.getCount().ToString()+CLang.getInstance().get(Dictionary.K_COUNT), getLabelStyle());

				}
				
			} else {
				
				Rect cellRectangle = new Rect(offsetX,
											  offsetY,
											  (float)CellSettings.cellWidth,
											  (float)CellSettings.cellHeight);

				//if (drawIcon)
				//GUI.DrawTexture(cellRectangle, resource.icon);

				if (item.item.getCount() > 1) {

					Rect labelRectangle = new Rect(cellRectangle.x+4,
												   cellRectangle.y+cellRectangle.height*item.item.getSize().getHeight()-22.0f,
												   cellRectangle.width*item.item.getSize().getWidth()-8,
												   20.0f);

					GUI.Label(labelRectangle, item.item.getCount().ToString() + CLang.getInstance().get(Dictionary.K_COUNT), getLabelStyle());

				}
				
			}
			
		}
		
		
	}
	
}