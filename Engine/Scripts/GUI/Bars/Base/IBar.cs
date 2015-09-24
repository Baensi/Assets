using System;

namespace Engine.EGUI.Bars {
	
	public interface IBar {
		
		float getValue();
		void  setValue(float value);
		
		float getMax();
		void  setMax(float max);
		
	}
	
}